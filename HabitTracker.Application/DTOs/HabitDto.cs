namespace HabitTracker.Application.DTOs;

public class HabitDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int CurrentStreak { get; set; }
    public int TotalCompletions { get; set; }
}
