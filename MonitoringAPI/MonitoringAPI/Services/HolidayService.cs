using System.Net.Http;
using System.Text.Json;

public class HolidayService : IHolidayService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public HolidayService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["AbstractApi:ApiKey"];
    }


    public async Task<bool> IsHolidayAsync(DateTime date, string countryCode)
    {
        var url = $"https://holidays.abstractapi.com/v1/?api_key={_apiKey}&country={countryCode}&year={date.Year}&month={date.Month}&day={date.Day}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return false;

        var json = await response.Content.ReadAsStringAsync();
        var holidays = JsonSerializer.Deserialize<List<HolidayDto>>(json);
        return holidays != null && holidays.Count > 0;
    }
}