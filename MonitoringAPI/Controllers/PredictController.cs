using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

[Authorize(Policy = "User")]
[ApiController]
[Route("[controller]")]
public class PredictController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _predictionApiUrl;

    public PredictController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _predictionApiUrl = configuration["PredictionApi"] ??
            throw new InvalidOperationException("Prediction API URL not configured");
    }

    [HttpPost]
    public async Task<IActionResult> Predict([FromBody] PredictionRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_predictionApiUrl}/predict", new
            {
                date = request.Date.ToString("yyyy-MM-dd")
            });

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode,
                    $"Prediction API returned {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PredictionResponse>(content);

            if (!string.IsNullOrEmpty(result.Error))
            {
                return BadRequest(new { result.Error, result.Details });
            }

            return Ok(new
            {
                Date = result.Date,
                PredictedTemperature = result.PredictedTemperature,
                IsHoliday = result.IsHoliday,
                PredictedWasteCount = result.PredictedWasteCount
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Error = "Failed to get prediction",
                Details = ex.Message
            });
        }
    }

    [HttpPost("mock")]
    public async Task<IActionResult> MockPredict([FromBody] PredictionRequest request)
    {
        if (request.Date.Date <= DateTime.Today)
        {
            return BadRequest(new
            {
                Error = "Invalid date",
                Details = "Date cannot be in the past"
            });
        }

        var random = new Random();
        var isChristmas = request.Date.Month == 12 && request.Date.Day is 25 or 26;

        var result = new PredictionResponse
        {
            Date = request.Date.ToString("yyyy-MM-dd"),
            PredictedTemperature = Math.Round(10 + random.NextDouble() * 20, 1), // 10-30°C
            IsHoliday = isChristmas,
            PredictedWasteCount = random.Next(20, 151) // 20-150
        };

        return Ok(new
        {
            result.Date,
            result.PredictedTemperature,
            result.IsHoliday,
            result.PredictedWasteCount
        });
    }
}