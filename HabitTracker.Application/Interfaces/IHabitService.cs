using HabitTracker.Application.DTOs;
using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Interfaces;

public interface IHabitService
{
    Task<IEnumerable<HabitDto>> GetAllHabitsAsync();
    Task<Habit> CreateHabitAsync(CreateHabitDto dto);
    Task DeleteHabitAsync(int id);
    Task MarkHabitCompleteAsync(int habitId);
}

