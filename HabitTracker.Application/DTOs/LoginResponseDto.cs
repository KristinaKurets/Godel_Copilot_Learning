namespace HabitTracker.Application.DTOs;

public class LoginResponseDto
{
    public string Username { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
