using HabitTracker.Application.Interfaces;

namespace HabitTracker.Application.Services;

public class StreakCalculator : IStreakCalculator
{
    public int CalculateCurrentStreak(IEnumerable<DateTime> completionDates)
    {
        if (completionDates == null || !completionDates.Any())
        {
            return 0;
        }

        var sortedDates = completionDates
            .Select(d => d.Date)
            .Distinct()
            .OrderByDescending(d => d)
            .ToList();

        var today = DateTime.UtcNow.Date;
        var yesterday = today.AddDays(-1);

        // Determine starting point: today if completed, otherwise yesterday
        DateTime startDate;
        if (sortedDates.Contains(today))
        {
            startDate = today;
        }
        else if (sortedDates.Contains(yesterday))
        {
            startDate = yesterday;
        }
        else
        {
            // No recent completions, streak is 0
            return 0;
        }

        // Count consecutive days backwards from start date
        int streak = 0;
        var expectedDate = startDate;

        while (sortedDates.Contains(expectedDate))
        {
            streak++;
            expectedDate = expectedDate.AddDays(-1);
        }

        return streak;
    }
}