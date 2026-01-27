using HabitTracker.Application.DTOs;
using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Interfaces;

public interface IHabitRepository
{
    Task<Habit> CreateHabit(CreateHabitDto dto);
    Task DeleteHabit(int id);
    Task MarkHabitComplete(int habitId);
    Task<IEnumerable<HabitDto>> GetAllHabits();
}