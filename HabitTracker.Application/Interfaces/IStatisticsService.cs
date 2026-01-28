using HabitTracker.Application.DTOs;

namespace HabitTracker.Application.Interfaces;

public interface IStatisticsService
{
    Task<StatisticsDto> GetStatisticsAsync();
}
