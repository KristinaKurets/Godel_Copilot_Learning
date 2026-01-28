namespace HabitTracker.Application.DTOs;

public class CurrentUserDto
{
    public string Username { get; set; } = string.Empty;
    public bool IsAuthenticated { get; set; }
}
