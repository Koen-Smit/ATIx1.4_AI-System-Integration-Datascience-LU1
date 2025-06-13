from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import pandas as pd
import joblib
from datetime import datetime
import numpy as np

app = FastAPI()

# Model laden
try:
    model = joblib.load('waste_prediction_model.joblib')
except Exception as e:
    model = None
    print(f"Model kon niet geladen worden: {e}")

class PredictionRequest(BaseModel):
    Postcode: str
    Datum: str  # DD-MM-YYYY
    Temperatuur: int = 20  # default waarde
    Windrichting: str = "NW"  # default waarde
    Weather_description: str = "Zonnig"  # default waarde

@app.post("/predict")
async def predict(request: PredictionRequest):
    if not model:
        raise HTTPException(status_code=500, detail="Model niet geladen")
    
    try:
        # Maak een complete input dataframe met ALLE benodigde features
        input_data = pd.DataFrame([{
            'Postcode': request.Postcode,
            'Windrichting': request.Windrichting,
            'Temperatuur': request.Temperatuur,
            'Weather description': request.Weather_description,
            'DagVanDeWeek': datetime.strptime(request.Datum, '%d-%m-%Y').weekday(),
            'Maand': datetime.strptime(request.Datum, '%d-%m-%Y').month
        }])
        
        # Voorspelling maken
        prediction = int(round(model.predict(input_data)[0]))
        priority = "laag" if prediction <= 3 else "gemiddeld" if prediction <= 7 else "hoog"
        
        return {
            "voorspelling": prediction,
            "prioriteit": priority,
            "gebruikte_features": input_data.to_dict()
        }
    except Exception as e:
        raise HTTPException(status_code=400, detail=f"Voorspellingsfout: {str(e)}")