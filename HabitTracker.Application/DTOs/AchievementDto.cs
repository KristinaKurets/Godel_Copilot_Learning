namespace HabitTracker.Application.DTOs;

public class AchievementDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool IsUnlocked { get; set; }
    public int CurrentProgress { get; set; }
    public int RequiredProgress { get; set; }
}
