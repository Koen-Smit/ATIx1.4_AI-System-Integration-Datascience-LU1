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
    public async Task<IActionResult> Create([FromBody] CreateTrashDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var trash = new Trash
        {
            DateCollected = dto.DateCollected,
            TypeAfval = dto.TypeAfval,
            WindRichting = dto.WindRichting,
            Temperatuur = dto.Temperatuur,
            WeerOmschrijving = dto.WeerOmschrijving,
            Confidence = dto.Confidence,
            CameraId = dto.CameraId
        };

        var created = await _repo.AddAsync(trash);

        var result = new TrashDTO
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
        };

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}

