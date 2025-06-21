using System.Text.Json.Serialization;

public class PredictionRequest
{
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
}