using HabitTracker.Application.DTOs;
using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Entities;

namespace HabitTracker.Application.Services;

public class HabitService : IHabitService
{
    private readonly IHabitRepository _habitRepository;

    public HabitService(IHabitRepository habitRepository)
    {
        _habitRepository = habitRepository;
    }

    public async Task<IEnumerable<HabitDto>> GetAllHabitsAsync()
    {
        return await _habitRepository.GetAllHabits();
    }

    public async Task<Habit> CreateHabitAsync(CreateHabitDto dto)
    {
        return await _habitRepository.CreateHabit(dto);
    }

    public async Task DeleteHabitAsync(int id)
    {
        await _habitRepository.DeleteHabit(id);
    }

    public async Task MarkHabitCompleteAsync(int habitId)
    {
        await _habitRepository.MarkHabitComplete(habitId);
    }
}
