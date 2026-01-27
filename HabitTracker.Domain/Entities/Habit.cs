namespace HabitTracker.Domain.Entities;

public class Habit
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    
    public ICollection<HabitCompletion> Completions { get; set; } = new List<HabitCompletion>();
}