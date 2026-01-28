using HabitTracker.Application.DTOs;

namespace HabitTracker.Application.Interfaces;

public interface IAchievementService
{
    Task<IEnumerable<AchievementDto>> GetAchievementsAsync();
}
