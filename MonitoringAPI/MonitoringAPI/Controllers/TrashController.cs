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
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _repo.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [Authorize]
    [HttpGet("bydate")]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
        => Ok(await _repo.GetByDateAsync(date));

    [Authorize]
    [HttpGet("after")]
    public async Task<IActionResult> GetAfterDate([FromQuery] DateTime afterDate)
        => Ok(await _repo.GetAfterDateAsync(afterDate));

    [Authorize]
    [HttpGet("before")]
    public async Task<IActionResult> GetBeforeDate([FromQuery] DateTime beforeDate)
        => Ok(await _repo.GetBeforeDateAsync(beforeDate));

    [Authorize]
    [HttpGet("type")]
    public async Task<IActionResult> GetByType([FromQuery] string type)
        => Ok(await _repo.GetByTypeAsync(type));

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
