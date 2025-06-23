using Microsoft.AspNetCore.Mvc;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public class SensorItem
{
    public string Naam_van_camera { get; set; } = "camera1";
    public float Longitude { get; set; } = 4.775072f;
    public float Latitude { get; set; } = 51.587533f;
    public string Postcode { get; set; } = "4811ET";
    public DateTime Tijd { get; set; }
    public string Type_afval { get; set; }
    public string Wind_richting { get; set; }
    public int Temperatuur { get; set; }
    public string Weather_description { get; set; }
    public float Confidence { get; set; }
}

public class SensorItemController : ControllerBase
{
    private static readonly Random random = new Random();
    private static readonly string[] afvalTypes = { "plastic", "papier", "glas", "metaal" };
    private static readonly string[] windRichtingen = { "NW", "NO", "ZW", "ZO", "N", "O", "Z", "W" };
    private static readonly string[] weatherDescriptions = { "bewolkt", "regen", "zonnig", "licht bewolkt" };

    [HttpGet]
    [Route("sensor")]
    public ActionResult<List<SensorItem>> GetSensorData([FromQuery] int count = 1)
    {
        // At least 1, max 20
        count = Math.Min(Math.Max(1, count), 20);

        var items = new List<SensorItem>();

        for (int i = 0; i < count; i++)
        {
            var item = new SensorItem
            {
                Tijd = GenerateRandomDateTime(),
                Type_afval = afvalTypes[random.Next(afvalTypes.Length)],
                Wind_richting = windRichtingen[random.Next(windRichtingen.Length)],
                Temperatuur = random.Next(10, 31), // 10-30
                Weather_description = weatherDescriptions[random.Next(weatherDescriptions.Length)],
                Confidence = (float)Math.Round(random.NextDouble() * 0.87 + 0.10, 2) // 0.10-0.97
            };
            items.Add(item);
        }

        return Ok(items);
    }

    private DateTime GenerateRandomDateTime()
    {
        // Tussen nu en 1 uur geleden
        DateTime now = DateTime.Now;
        TimeSpan timeSpan = now - now.AddHours(-1);
        TimeSpan newSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
        return now - newSpan;
    }
}