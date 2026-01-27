namespace HabitTracker.Domain.Entities;

public class HabitCompletion
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public DateTime CompletedDate { get; set; }    
    public Habit Habit { get; set; } = null!;
}
