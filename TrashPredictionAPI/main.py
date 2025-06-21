from fastapi import FastAPI
from pydantic import BaseModel
from datetime import date
import pandas as pd
import numpy as np
from xgboost import XGBRegressor
import joblib

app = FastAPI()

# Load the trained model (using native XGBoost format for better compatibility)
try:
    model = XGBRegressor()
    model.load_model('waste_model_85plus.json')
    print("Model loaded successfully from .json format")
except Exception as e:
    print(f"Couldn't load .json model, falling back to joblib: {str(e)}")
    try:
        model = joblib.load('waste_model_85plus.joblib')
        print("Model loaded successfully from .joblib format")
    except Exception as e:
        print(f"Failed to load model: {str(e)}")
        raise

# Load daily data
daily = pd.read_csv("daily.csv", parse_dates=['timestamp'])

# API Input
class PredictionRequest(BaseModel):
    date: date

# Feature Engineering Function
def prepare_features(input_date: pd.Timestamp) -> pd.DataFrame:
    input_features = {
        'dayofweek': input_date.dayofweek,
        'weekofyear': input_date.isocalendar().week,
        'month': input_date.month,
        'dayofyear': input_date.dayofyear,
        'is_weekend': int(input_date.dayofweek in [5, 6]),
        'is_holiday': int(input_date.strftime('%d-%m') in ['01-01', '27-04', '25-12', '26-12']),
        '7day_avg': daily['count'].tail(7).mean(),
        '14day_avg': daily['count'].tail(14).mean(),
        '28day_avg': daily['count'].tail(28).mean(),
        '90day_avg': daily['count'].tail(90).mean(),
        '7day_std': daily['count'].tail(7).std(),
        '14day_std': daily['count'].tail(14).std(),
        'lag_1': daily['count'].iloc[-1],
        'lag_2': daily['count'].iloc[-2],
        'lag_3': daily['count'].iloc[-3],
        'lag_7': daily['count'].iloc[-7],
        'trend': daily['count'].pct_change().iloc[-1],
        'rolling_max_14': daily['count'].tail(14).max(),
        'rolling_min_14': daily['count'].tail(14).min()
    }
    return pd.DataFrame([input_features])

# Prediction endpoint
@app.post("/predict")
def predict(request: PredictionRequest):
    try:
        input_date = pd.to_datetime(request.date)
        features = prepare_features(input_date)
        
        # Ensure feature order matches training
        feature_cols = [
            'dayofweek', 'weekofyear', 'month', 'dayofyear',
            'is_weekend', 'is_holiday',
            '7day_avg', '14day_avg', '28day_avg', '90day_avg',
            '7day_std', '14day_std',
            'lag_1', 'lag_2', 'lag_3', 'lag_7',
            'trend', 'rolling_max_14', 'rolling_min_14'
        ]
        features = features[feature_cols]
        
        prediction = model.predict(features)[0]
        return {
            "date": request.date.isoformat(),
            "predicted_count": round(prediction)
        }
    except Exception as e:
        return {
            "error": str(e),
            "details": "Please ensure the date is valid and the model is properly loaded"
        }

# Health check  
@app.get("/")
def read_root():
    return {
        "message": "Waste Detection Prediction API",
        "status": "operational",
        "model_loaded": model is not None
    }