using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("trash")]
public class TrashController : ControllerBase
{
    private readonly ITrashRepository _repo;

    public TrashController(ITrashRepository repo)
    {
        _repo = repo;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetFiltered(
        [FromQuery] DateTime? date = null,
        [FromQuery] DateTime? after = null,
        [FromQuery] DateTime? before = null,
        [FromQuery] string? type = null)
    {
        var results = await _repo.GetFilteredAsync(date, after, before, type);
        return Ok(results);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _repo.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Trash trash)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _repo.AddAsync(trash);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

}
