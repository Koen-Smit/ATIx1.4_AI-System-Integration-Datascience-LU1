using System.Text.Json.Serialization;

public class PredictionResponse
{
    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("predicted_temperature")]
    public double PredictedTemperature { get; set; }

    [JsonPropertyName("is_holiday")]
    public bool IsHoliday { get; set; }

    [JsonPropertyName("predicted_waste_count")]
    public int PredictedWasteCount { get; set; }

    [JsonPropertyName("error")]
    public string Error { get; set; }

    [JsonPropertyName("details")]
    public string Details { get; set; }
}