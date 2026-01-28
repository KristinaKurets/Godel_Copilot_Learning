using HabitTracker.Application.DTOs;
using HabitTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public ActionResult<LoginResponseDto> Login([FromBody] LoginRequestDto request)
    {
        var response = _authService.Login(request.Username);

        if (string.IsNullOrEmpty(response.Token))
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("current-user")]
    public ActionResult<CurrentUserDto> GetCurrentUser()
    {
        // Try to get token from Authorization header
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        string? token = null;

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            token = authHeader.Substring("Bearer ".Length).Trim();
        }

        var currentUser = _authService.GetCurrentUser(token);
        return Ok(currentUser);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        string? token = null;

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            token = authHeader.Substring("Bearer ".Length).Trim();
        }

        _authService.Logout(token);
        return Ok(new { message = "Logged out successfully" });
    }
}
