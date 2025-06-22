using Microsoft.AspNetCore.Authorization;
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

    [HttpPost]
    public async Task<IActionResult> CreateRandom([FromQuery] int count = 1)
    {
        count = Math.Clamp(count, 1, 20);

        var httpClient = new HttpClient();
        var sensorData = await httpClient.GetFromJsonAsync<List<SensorData>>($"https://localhost:7088/sensor?count={count}");

        if (sensorData == null || !sensorData.Any())
        {
            return BadRequest("Failed to fetch random data from sensor API");
        }

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
                CameraId = 1
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

}


