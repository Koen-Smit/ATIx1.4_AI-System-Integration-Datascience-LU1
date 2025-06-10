using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepo;
    private readonly JwtTokenService _tokenService;

    public AccountController(IAccountRepository accountRepo, JwtTokenService tokenService)
    {
        _accountRepo = accountRepo;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var existing = await _accountRepo.GetByEmailAsync(dto.Email);
        if (existing != null)
            return BadRequest("Email already in use");

        var user = new User { Email = dto.Email, Username = dto.Username };
        var registered = await _accountRepo.RegisterAsync(user, dto.Password);
        return Ok(new { registered.Id, registered.Email, registered.Username });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return BadRequest("Already logged in.");
        }

        var user = await _accountRepo.GetByEmailAsync(dto.Email);
        if (user == null)
            return Unauthorized("Invalid credentials");

        bool validPassword = await _accountRepo.CheckPasswordAsync(user, dto.Password);
        if (!validPassword)
            return Unauthorized("Invalid credentials");

        var token = _tokenService.GenerateToken(user);
        return Ok(new { token });
    }

    [HttpGet("username")]
    [Authorize]
    public IActionResult GetUsername()
    {
        var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized();
        }

        return Ok(new { Username = username });
    }


    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        return Ok(new { message = "Logged out successfully." });
    }
}
