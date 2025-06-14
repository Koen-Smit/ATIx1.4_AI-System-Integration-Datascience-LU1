using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Policy = "User")]
[ApiController]
[Route("[controller]")]
public class CameraController : ControllerBase
{
    private readonly ICameraRepository _repo;

    public CameraController(ICameraRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _repo.GetAllAsync());


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Camera camera)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _repo.AddAsync(camera);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var camera = await _repo.GetByIdAsync(id);
        return camera == null ? NotFound() : Ok(camera);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repo.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
