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
        _predictionApiUrl = configuration["PredictionApi:BaseUrl"] ??
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

    [HttpGet("health")]
    public async Task<IActionResult> CheckHealth()
    {
        try
        {
            var response = await _httpClient.GetAsync(_predictionApiUrl);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode,
                    $"Prediction API returned {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var healthStatus = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

            return Ok(healthStatus);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Error = "Failed to check prediction API health",
                Details = ex.Message
            });
        }
    }
}