namespace HabitTracker.Application.DTOs;

public class UserProfileDto
{
    public string Username { get; set; } = string.Empty;
    public DateTime? MemberSince { get; set; }
    public int TotalCompletions { get; set; }
    public int AchievementsUnlocked { get; set; }
    public int Level { get; set; }
}
