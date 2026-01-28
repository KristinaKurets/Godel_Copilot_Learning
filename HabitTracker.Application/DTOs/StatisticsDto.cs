namespace HabitTracker.Application.DTOs;

public class StatisticsDto
{
    public int TotalHabits { get; set; }
    public int HabitsWithActiveStreaks { get; set; }
    public int CompletionsThisWeek { get; set; }
    public int CompletionsThisMonth { get; set; }
    public int LongestStreak { get; set; }
}
