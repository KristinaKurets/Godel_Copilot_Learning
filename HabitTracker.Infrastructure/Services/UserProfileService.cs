using HabitTracker.Application.DTOs;
using HabitTracker.Application.Interfaces;
using HabitTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Infrastructure.Services;

public class UserProfileService : IUserProfileService
{
    private readonly ApplicationDbContext _context;
    private readonly IAchievementService _achievementService;

    public UserProfileService(ApplicationDbContext context, IAchievementService achievementService)
    {
        _context = context;
        _achievementService = achievementService;
    }

    public async Task<UserProfileDto> GetUserProfileAsync()
    {
        // Get all habits (including inactive to find first creation date)
        var habits = await _context.Habits.ToListAsync();

        // Find when user started (date of first habit creation)
        DateTime? memberSince = null;
        if (habits.Any())
        {
            memberSince = habits.Min(h => h.CreatedAt);
        }

        // Calculate total completions across all habits
        var totalCompletions = await _context.HabitCompletions.CountAsync();

        // Get achievements and count unlocked ones
        var achievements = await _achievementService.GetAchievementsAsync();
        var achievementsUnlocked = achievements.Count(a => a.IsUnlocked);

        // Calculate level based on completions (10 completions = 1 level)
        var level = totalCompletions / 10;

        return new UserProfileDto
        {
            Username = "Habit Hero",
            MemberSince = memberSince,
            TotalCompletions = totalCompletions,
            AchievementsUnlocked = achievementsUnlocked,
            Level = level
        };
    }
}
