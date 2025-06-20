from fastapi import FastAPI
from pydantic import BaseModel
from datetime import date
import pandas as pd
import numpy as np
from xgboost import XGBRegressor
import joblib
from typing import Dict

app = FastAPI()

# Load models and data
try:
    model = XGBRegressor()
    model.load_model('waste_model_with_weather.json')
    print("Main model loaded successfully from .json format")
except Exception as e:
    print(f"Couldn't load .json model, falling back to joblib: {str(e)}")
    try:
        model = joblib.load('waste_model_with_weather.joblib')
        print("Main model loaded successfully from .joblib format")
    except Exception as e:
        print(f"Failed to load main model: {str(e)}")
        raise

# Load daily data
daily = pd.read_csv("daily_with_weather.csv", parse_dates=['timestamp'])

# API Input
class PredictionRequest(BaseModel):
    date: date

# Feature Engineering Function
def prepare_features(input_date: pd.Timestamp) -> Dict[str, float]:
    # Calculate temperature based on seasonal patterns (same as generation logic)
    day_of_year = input_date.dayofyear
    base_temp = 10
    temp_variation = 15 * np.sin(2 * np.pi * (day_of_year - 105) / 365)
    temperature = base_temp + temp_variation + np.random.normal(0, 3)
    
    # Determine if holiday
    date_str = input_date.strftime('%d-%m')
    is_holiday = date_str in ['01-01', '27-04', '25-12', '26-12']
    
    features = {
        'dayofweek': input_date.dayofweek,
        'weekofyear': input_date.isocalendar().week,
        'month': input_date.month,
        'dayofyear': day_of_year,
        'is_weekend': int(input_date.dayofweek in [5, 6]),
        'is_holiday': int(is_holiday),
        'temperature': temperature,
        'temp_7day_avg': daily['temperature'].tail(7).mean(),
        'temp_14day_avg': daily['temperature'].tail(14).mean(),
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
        'temp_lag_1': daily['temperature'].iloc[-1],
        'temp_lag_2': daily['temperature'].iloc[-2],
        'temp_lag_7': daily['temperature'].iloc[-7],
        'trend': daily['count'].pct_change().iloc[-1],
        'rolling_max_14': daily['count'].tail(14).max(),
        'rolling_min_14': daily['count'].tail(14).min()
    }
    return features

# Prediction endpoint
@app.post("/predict")
def predict(request: PredictionRequest):
    try:
        input_date = pd.to_datetime(request.date)
        features = prepare_features(input_date)
        
        # Prepare feature DataFrame in correct order
        feature_cols = [
            'dayofweek', 'weekofyear', 'month', 'dayofyear',
            'is_weekend', 'is_holiday',
            'temperature', 'temp_7day_avg', 'temp_14day_avg',
            '7day_avg', '14day_avg', '28day_avg', '90day_avg',
            '7day_std', '14day_std',
            'lag_1', 'lag_2', 'lag_3', 'lag_7',
            'temp_lag_1', 'temp_lag_2', 'temp_lag_7',
            'trend', 'rolling_max_14', 'rolling_min_14'
        ]
        features_df = pd.DataFrame([features])[feature_cols]
        
        # Predict waste count
        predicted_count = round(model.predict(features_df)[0])
        
        return {
            "date": request.date.isoformat(),
            "predicted_temperature": round(features['temperature'], 1),
            "is_holiday": bool(features['is_holiday']),
            "predicted_waste_count": predicted_count
        }
    except Exception as e:
        return {
            "error": str(e),
            "details": "Please ensure the date is valid and models are properly loaded"
        }

# Health check  
@app.get("/")
def read_root():
    return {
        "message": "Waste Prediction API - Just provide a date",
        "status": "operational",
        "model_loaded": model is not None,
        "example_request": {
            "date": "2023-06-15"
        },
        "example_response": {
            "date": "2023-06-15",
            "predicted_temperature": 18.5,
            "is_holiday": False,
            "predicted_waste_count": 42
        }
    }