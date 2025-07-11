﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = "User")]
[ApiController]
[Route("trash")]
public class TrashController : ControllerBase
{
    private readonly ITrashRepository _repo;

    public TrashController(ITrashRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetFiltered(
        [FromQuery] DateTime? date = null,
        [FromQuery] DateTime? after = null,
        [FromQuery] DateTime? before = null,
        [FromQuery] string? type = null,
        [FromQuery] string? dagCategorie = null)
    {
        var results = await _repo.GetFilteredAsync(date, after, before, type, dagCategorie);
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _repo.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    //[HttpPost]
    //public async Task<IActionResult> Create([FromQuery] int count = 1)
    //{
    //    count = Math.Clamp(count, 1, 20);

    //    var httpClient = new HttpClient();
    //    var sensorData = await httpClient.GetFromJsonAsync<List<SensorData>>($"https://localhost:7088/sensor?count={count}");

    //    if (sensorData == null || !sensorData.Any())
    //    {
    //        return BadRequest("Failed to fetch random data from sensor API");
    //    }

    //    var results = new List<TrashDTO>();
    //    foreach (var item in sensorData)
    //    {
    //        var dateCollected = item.Tijd;
    //        var dagCategorie = dateCollected.DayOfWeek switch
    //        {
    //            DayOfWeek.Saturday or DayOfWeek.Sunday => "Weekend",
    //            _ => "Werkdag"
    //        };

    //        var trash = new Trash
    //        {
    //            DateCollected = item.Tijd,
    //            DagCategorie = dagCategorie,
    //            TypeAfval = item.Type_afval,
    //            WindRichting = item.Wind_richting,
    //            Temperatuur = item.Temperatuur,
    //            WeerOmschrijving = item.Weather_description,
    //            Confidence = item.Confidence,
    //            CameraId = 1
    //        };

    //        var created = await _repo.AddAsync(trash);

    //        results.Add(new TrashDTO
    //        {
    //            Id = created.Id,
    //            DateCollected = created.DateCollected,
    //            DagCategorie = created.DagCategorie,
    //            TypeAfval = created.TypeAfval,
    //            WindRichting = created.WindRichting,
    //            Temperatuur = created.Temperatuur,
    //            WeerOmschrijving = created.WeerOmschrijving,
    //            Confidence = created.Confidence,
    //            CameraId = created.CameraId
    //        });
    //    }

    //    return Ok(results);
    //}
    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] int count = 1)
    {
        count = Math.Clamp(count, 1, 20);

        // Generate random sensor data directly (no HTTP call needed)
        var sensorData = GenerateRandomSensorData(count);

        var results = new List<TrashDTO>();
        foreach (var item in sensorData)
        {
            var dateCollected = item.Tijd;
            var dagCategorie = dateCollected.DayOfWeek switch
            {
                DayOfWeek.Saturday or DayOfWeek.Sunday => "Weekend",
                _ => "Werkdag"
            };

            var trash = new Trash
            {
                DateCollected = item.Tijd,
                DagCategorie = dagCategorie,
                TypeAfval = item.Type_afval,
                WindRichting = item.Wind_richting,
                Temperatuur = item.Temperatuur,
                WeerOmschrijving = item.Weather_description,
                Confidence = item.Confidence,
                CameraId = 1 // Hardcoded for now
            };

            var created = await _repo.AddAsync(trash);

            results.Add(new TrashDTO
            {
                Id = created.Id,
                DateCollected = created.DateCollected,
                DagCategorie = created.DagCategorie,
                TypeAfval = created.TypeAfval,
                WindRichting = created.WindRichting,
                Temperatuur = created.Temperatuur,
                WeerOmschrijving = created.WeerOmschrijving,
                Confidence = created.Confidence,
                CameraId = created.CameraId
            });
        }

        return Ok(results);
    }

    private List<SensorData> GenerateRandomSensorData(int count)
    {
        var random = new Random();
        var afvalTypes = new[] { "plastic", "papier", "glas", "metaal" };
        var windRichtingen = new[] { "NW", "NO", "ZW", "ZO", "N", "O", "Z", "W" };
        var weatherDescriptions = new[] { "bewolkt", "regen", "zonnig", "licht bewolkt" };

        var items = new List<SensorData>();

        for (int i = 0; i < count; i++)
        {
            items.Add(new SensorData
            {
                Naam_van_camera = "camera1",
                Longitude = 4.775072f,
                Latitude = 51.587533f,
                Postcode = "4811ET",
                Tijd = GenerateRandomDateTime(),
                Type_afval = afvalTypes[random.Next(afvalTypes.Length)],
                Wind_richting = windRichtingen[random.Next(windRichtingen.Length)],
                Temperatuur = random.Next(10, 31), // 10-30°C
                Weather_description = weatherDescriptions[random.Next(weatherDescriptions.Length)],
                Confidence = (float)Math.Round(random.NextDouble() * 0.87 + 0.10, 2) // 0.10-0.97
            });
        }

        return items;
    }

    private DateTime GenerateRandomDateTime()
    {
        var random = new Random();
        DateTime now = DateTime.Now;
        TimeSpan timeSpan = now - now.AddHours(-1);
        TimeSpan newSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
        return now - newSpan;
    }
}


