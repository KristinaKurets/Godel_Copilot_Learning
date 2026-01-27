using HabitTracker.Application.DTOs;
using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Infrastructure.Repositories;

public class HabitRepository : IHabitRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IStreakCalculator _streakCalculator;

    public HabitRepository(ApplicationDbContext context, IStreakCalculator streakCalculator)
    {
        _context = context;
        _streakCalculator = streakCalculator;
    }

    public async Task<Habit> CreateHabit(CreateHabitDto dto)
    {
        var habit = new Habit
        {
            Name = dto.Name,
            Category = dto.Category,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();
        return habit;
    }

    public async Task DeleteHabit(int id)
    {
        var habit = await _context.Habits.FindAsync(id);
        if (habit != null)
        {
            _context.Habits.Remove(habit);
            await _context.SaveChangesAsync();
        }
    }

    public async Task MarkHabitComplete(int habitId)
    {
        var today = DateTime.UtcNow.Date;
        
        var existingCompletion = await _context.HabitCompletions
            .FirstOrDefaultAsync(c => c.HabitId == habitId && c.CompletedDate.Date == today);

        if (existingCompletion == null)
        {
            var completion = new HabitCompletion
            {
                HabitId = habitId,
                CompletedDate = DateTime.UtcNow
            };

            _context.HabitCompletions.Add(completion);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<HabitDto>> GetAllHabits()
    {
        var habits = await _context.Habits
            .Include(h => h.Completions)
            .Where(h => h.IsActive)
            .ToListAsync();

        var habitDtos = new List<HabitDto>();

        foreach (var habit in habits)
        {
            var completionDates = habit.Completions.Select(c => c.CompletedDate);
            var currentStreak = _streakCalculator.CalculateCurrentStreak(completionDates);
            var totalCompletions = habit.Completions.Count;

            habitDtos.Add(new HabitDto
            {
                Id = habit.Id,
                Name = habit.Name,
                Category = habit.Category,
                CurrentStreak = currentStreak,
                TotalCompletions = totalCompletions
            });
        }

        return habitDtos;
    }
}

