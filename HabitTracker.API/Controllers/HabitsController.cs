using HabitTracker.Application.DTOs;
using HabitTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitsController : ControllerBase
{
    private readonly IHabitService _habitService;

    public HabitsController(IHabitService habitService)
    {
        _habitService = habitService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HabitDto>>> GetAllHabits()
    {
        var habits = await _habitService.GetAllHabitsAsync();
        return Ok(habits);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateHabit(CreateHabitDto dto)
    {
        var createdHabit = await _habitService.CreateHabitAsync(dto);
        return CreatedAtAction(nameof(GetAllHabits), new { id = createdHabit.Id }, createdHabit.Id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHabit(int id)
    {
        await _habitService.DeleteHabitAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> MarkHabitComplete(int id)
    {
        await _habitService.MarkHabitCompleteAsync(id);
        return NoContent();
    }
}

