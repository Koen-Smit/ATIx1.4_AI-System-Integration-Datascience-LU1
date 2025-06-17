import pandas as pd
import numpy as np
from datetime import datetime, timedelta
from sklearn.metrics import r2_score, mean_absolute_error
from xgboost import XGBRegressor
import joblib

# 1. Generate dataset
def generate_extended_dataset(years=3):
    base_date = datetime(2022, 1, 1)
    days = 365 * years
    dates = pd.date_range(base_date, periods=days)
    seasonal = 15 + 5 * np.sin(2 * np.pi * (dates.dayofyear - 105) / 365)
    weekly = np.where(dates.dayofweek.isin([4, 5]), 1.5, 1.0)
    holidays = {'01-01': 0.6, '27-04': 1.3, '25-12': 0.5, '26-12': 0.5}
    daily_counts = []

    for i, date in enumerate(dates):
        base = seasonal[i]
        count = base * weekly[i]
        date_str = date.strftime('%d-%m')
        if date_str in holidays:
            count *= holidays[date_str]
        count = int(max(5, np.random.poisson(count)))
        daily_counts.append(count)

    records = []
    for i, date in enumerate(dates):
        count = daily_counts[i]
        hours = np.clip(np.random.normal(loc=12, scale=4, size=count), 6, 22)
        types = np.random.choice(
            ['plastic', 'organic', 'glass', 'paper', 'metal'],
            size=count,
            p=[0.4, 0.3, 0.1, 0.15, 0.05]
        )
        for h, t in zip(hours, types):
            records.append({
                'timestamp': date + timedelta(hours=h),
                'detected_object': t,
                'confidence_score': round(np.random.uniform(6, 9), 2)
            })
    return pd.DataFrame(records)

# 2. Preprocessing to get daily DataFrame
df = generate_extended_dataset()
daily = df.resample('D', on='timestamp').agg(
    count=('detected_object', 'size'),
    plastic=('detected_object', lambda x: (x == 'plastic').sum()),
    organic=('detected_object', lambda x: (x == 'organic').sum()),
    glass=('detected_object', lambda x: (x == 'glass').sum()),
    paper=('detected_object', lambda x: (x == 'paper').sum()),
    metal=('detected_object', lambda x: (x == 'metal').sum())
).reset_index()

# Feature engineering
daily['dayofweek'] = daily['timestamp'].dt.dayofweek
daily['weekofyear'] = daily['timestamp'].dt.isocalendar().week.astype(int)
daily['month'] = daily['timestamp'].dt.month
daily['dayofyear'] = daily['timestamp'].dt.dayofyear
daily['is_weekend'] = daily['dayofweek'].isin([5, 6]).astype(int)
daily['is_holiday'] = daily['timestamp'].dt.strftime('%d-%m').isin(
    ['01-01', '27-04', '25-12', '26-12']
).astype(int)

for window in [7, 14, 28, 90]:
    daily[f'{window}day_avg'] = daily['count'].rolling(window, min_periods=1).mean()
    daily[f'{window}day_std'] = daily['count'].rolling(window, min_periods=1).std().fillna(0)

for lag in [1, 2, 3, 7]:
    daily[f'lag_{lag}'] = daily['count'].shift(lag).bfill()

daily['trend'] = daily['count'].pct_change().fillna(0)
daily['rolling_max_14'] = daily['count'].rolling(14).max().bfill()
daily['rolling_min_14'] = daily['count'].rolling(14).min().bfill()

# 3. Features and target
feature_cols = [
    'dayofweek', 'weekofyear', 'month', 'dayofyear',
    'is_weekend', 'is_holiday',
    '7day_avg', '14day_avg', '28day_avg', '90day_avg',
    '7day_std', '14day_std',
    'lag_1', 'lag_2', 'lag_3', 'lag_7',
    'trend', 'rolling_max_14', 'rolling_min_14'
]
X = daily[feature_cols].astype(float)
y = daily['count']

# 4. Train/test split
split = int(len(X) * 0.8)
X_train, X_test = X.iloc[:split], X.iloc[split:]
y_train, y_test = y.iloc[:split], y.iloc[split:]

# 5. Train model
model = XGBRegressor(
    n_estimators=300,
    learning_rate=0.1,
    max_depth=8,
    subsample=0.8,
    colsample_bytree=0.8,
    random_state=42,
    tree_method="hist"  # Using hist for CPU compatibility
)

model.fit(X_train, y_train)
y_pred = model.predict(X_test)

# 6. Save model in both formats for flexibility
model.save_model('waste_model_85plus.json')  # XGBoost native format
joblib.dump(model, 'waste_model_85plus.joblib')  # Joblib format

# 7. Save daily DataFrame
daily.to_csv('daily.csv', index=False)

# 8. Evaluation
r2 = r2_score(y_test, y_pred)
mae = mean_absolute_error(y_test, y_pred)
print(f"✅ Model trained successfully")
print(f"✅ R²-score: {r2 * 100:.2f}%")
print(f"✅ MAE: {mae:.2f} objects average difference")
print("📦 Model saved in both .json and .joblib formats")
print("📊 Daily DataFrame saved as 'daily.csv'")