using HabitTracker.Application.DTOs;
using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Enums;
using HabitTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Infrastructure.Services;

public class AchievementService : IAchievementService
{
    private readonly ApplicationDbContext _context;
    private readonly IStreakCalculator _streakCalculator;

    public AchievementService(ApplicationDbContext context, IStreakCalculator streakCalculator)
    {
        _context = context;
        _streakCalculator = streakCalculator;
    }

    public async Task<IEnumerable<AchievementDto>> GetAchievementsAsync()
    {
        var habits = await _context.Habits
            .Include(h => h.Completions)
            .Where(h => h.IsActive)
            .ToListAsync();

        var totalHabits = habits.Count;
        var totalCompletions = habits.Sum(h => h.Completions.Count);

        // Calculate max streak across all habits
        var maxStreak = 0;
        foreach (var habit in habits)
        {
            var completionDates = habit.Completions.Select(c => c.CompletedDate);
            var streak = _streakCalculator.CalculateCurrentStreak(completionDates);
            if (streak > maxStreak)
            {
                maxStreak = streak;
            }
        }

        // Check for perfect week (all habits completed every day for 7 days)
        var perfectWeekProgress = await CalculatePerfectWeekProgress(habits);

        var achievements = new List<AchievementDto>();

        // First Habit
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.FirstHabit.ToString(),
            Name = "Getting Started",
            Description = "Create your first habit",
            Icon = "seedling",
            IsUnlocked = totalHabits >= 1,
            CurrentProgress = Math.Min(totalHabits, 1),
            RequiredProgress = 1
        });

        // 5 Habits
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.FiveHabits.ToString(),
            Name = "Habit Builder",
            Description = "Have 5 active habits at once",
            Icon = "star",
            IsUnlocked = totalHabits >= 5,
            CurrentProgress = Math.Min(totalHabits, 5),
            RequiredProgress = 5
        });

        // 10 Habits
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.TenHabits.ToString(),
            Name = "Habit Master",
            Description = "Have 10 active habits at once",
            Icon = "crown",
            IsUnlocked = totalHabits >= 10,
            CurrentProgress = Math.Min(totalHabits, 10),
            RequiredProgress = 10
        });

        // 7 Day Streak
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.SevenDayStreak.ToString(),
            Name = "Week Warrior",
            Description = "Complete a habit for 7 days in a row",
            Icon = "fire",
            IsUnlocked = maxStreak >= 7,
            CurrentProgress = Math.Min(maxStreak, 7),
            RequiredProgress = 7
        });

        // 10 Day Streak
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.TenDayStreak.ToString(),
            Name = "Perfect Ten",
            Description = "Complete a habit for 10 days in a row",
            Icon = "gem",
            IsUnlocked = maxStreak >= 10,
            CurrentProgress = Math.Min(maxStreak, 10),
            RequiredProgress = 10
        });

        // 30 Day Streak
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.ThirtyDayStreak.ToString(),
            Name = "Monthly Champion",
            Description = "Complete a habit for 30 days in a row",
            Icon = "trophy",
            IsUnlocked = maxStreak >= 30,
            CurrentProgress = Math.Min(maxStreak, 30),
            RequiredProgress = 30
        });

        // 60 Day Streak
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.SixtyDayStreak.ToString(),
            Name = "Unstoppable",
            Description = "Complete a habit for 60 days in a row",
            Icon = "rocket",
            IsUnlocked = maxStreak >= 60,
            CurrentProgress = Math.Min(maxStreak, 60),
            RequiredProgress = 60
        });

        // 90 Day Streak
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.NinetyDayStreak.ToString(),
            Name = "Legendary",
            Description = "Complete a habit for 90 days in a row",
            Icon = "medal",
            IsUnlocked = maxStreak >= 90,
            CurrentProgress = Math.Min(maxStreak, 90),
            RequiredProgress = 90
        });

        // 100 Completions
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.HundredCompletions.ToString(),
            Name = "Century Club",
            Description = "Reach 100 total completions",
            Icon = "hundred",
            IsUnlocked = totalCompletions >= 100,
            CurrentProgress = Math.Min(totalCompletions, 100),
            RequiredProgress = 100
        });

        // 250 Completions
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.TwoHundredFiftyCompletions.ToString(),
            Name = "Dedication",
            Description = "Reach 250 total completions",
            Icon = "sparkles",
            IsUnlocked = totalCompletions >= 250,
            CurrentProgress = Math.Min(totalCompletions, 250),
            RequiredProgress = 250
        });

        // 500 Completions
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.FiveHundredCompletions.ToString(),
            Name = "Epic Achievement",
            Description = "Reach 500 total completions",
            Icon = "zap",
            IsUnlocked = totalCompletions >= 500,
            CurrentProgress = Math.Min(totalCompletions, 500),
            RequiredProgress = 500
        });

        // Perfect Week
        achievements.Add(new AchievementDto
        {
            Id = AchievementType.PerfectWeek.ToString(),
            Name = "Perfect Week",
            Description = "Complete all habits every day for 7 days",
            Icon = "rainbow",
            IsUnlocked = perfectWeekProgress >= 7,
            CurrentProgress = perfectWeekProgress,
            RequiredProgress = 7
        });

        return achievements.OrderByDescending(a => a.IsUnlocked).ThenBy(a => a.Name);
    }

    private async Task<int> CalculatePerfectWeekProgress(List<Domain.Entities.Habit> habits)
    {
        if (habits.Count == 0)
        {
            return 0;
        }

        var today = DateTime.UtcNow.Date;
        var perfectDays = 0;

        // Check each day going backwards from today
        for (int daysBack = 0; daysBack < 7; daysBack++)
        {
            var checkDate = today.AddDays(-daysBack);
            var allHabitsCompletedOnDate = true;

            foreach (var habit in habits)
            {
                var completedOnDate = habit.Completions.Any(c => c.CompletedDate.Date == checkDate);
                if (!completedOnDate)
                {
                    allHabitsCompletedOnDate = false;
                    break;
                }
            }

            if (allHabitsCompletedOnDate)
            {
                perfectDays++;
            }
            else
            {
                // Break the streak if a day is not perfect
                break;
            }
        }

        return perfectDays;
    }
}
