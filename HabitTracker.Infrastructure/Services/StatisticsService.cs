using HabitTracker.Application.DTOs;
using HabitTracker.Application.Interfaces;
using HabitTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Infrastructure.Services;

public class StatisticsService : IStatisticsService
{
    private readonly ApplicationDbContext _context;
    private readonly IStreakCalculator _streakCalculator;

    public StatisticsService(ApplicationDbContext context, IStreakCalculator streakCalculator)
    {
        _context = context;
        _streakCalculator = streakCalculator;
    }

    public async Task<StatisticsDto> GetStatisticsAsync()
    {
        var now = DateTime.UtcNow;
        var today = now.Date;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
        var startOfMonth = new DateTime(today.Year, today.Month, 1);

        var habits = await _context.Habits
            .Include(h => h.Completions)
            .Where(h => h.IsActive)
            .ToListAsync();

        var totalHabits = habits.Count;

        var habitsWithActiveStreaks = 0;
        var longestStreak = 0;

        foreach (var habit in habits)
        {
            var completionDates = habit.Completions.Select(c => c.CompletedDate);
            var currentStreak = _streakCalculator.CalculateCurrentStreak(completionDates);

            if (currentStreak > 0)
            {
                habitsWithActiveStreaks++;
            }

            if (currentStreak > longestStreak)
            {
                longestStreak = currentStreak;
            }
        }

        var completionsThisWeek = await _context.HabitCompletions
            .Where(c => c.CompletedDate >= startOfWeek && c.CompletedDate < today.AddDays(1))
            .CountAsync();

        var completionsThisMonth = await _context.HabitCompletions
            .Where(c => c.CompletedDate >= startOfMonth && c.CompletedDate < today.AddDays(1))
            .CountAsync();

        return new StatisticsDto
        {
            TotalHabits = totalHabits,
            HabitsWithActiveStreaks = habitsWithActiveStreaks,
            CompletionsThisWeek = completionsThisWeek,
            CompletionsThisMonth = completionsThisMonth,
            LongestStreak = longestStreak
        };
    }
}
