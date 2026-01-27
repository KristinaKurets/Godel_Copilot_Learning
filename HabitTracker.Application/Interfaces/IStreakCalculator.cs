namespace HabitTracker.Application.Interfaces;

public interface IStreakCalculator
{
    int CalculateCurrentStreak(IEnumerable<DateTime> completionDates);
}
