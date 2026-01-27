using HabitTracker.Application.DTOs;
using HabitTracker.Application.Interfaces;
using HabitTracker.Application.Services;
using HabitTracker.Domain.Entities;
using Moq;

namespace HabitTracker.Tests;

public class HabitServiceTests
{
    private readonly Mock<IHabitRepository> _mockRepository;
    private readonly HabitService _service;

    public HabitServiceTests()
    {
        _mockRepository = new Mock<IHabitRepository>();
        _service = new HabitService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllHabitsAsync_ReturnsAllHabits()
    {
        // Arrange
        var expectedHabits = new List<HabitDto>
        {
            new HabitDto { Id = 1, Name = "Exercise", Category = "Fitness", CurrentStreak = 5, TotalCompletions = 10 },
            new HabitDto { Id = 2, Name = "Reading", Category = "Learning", CurrentStreak = 3, TotalCompletions = 15 }
        };

        _mockRepository
            .Setup(r => r.GetAllHabits())
            .ReturnsAsync(expectedHabits);

        // Act
        var result = await _service.GetAllHabitsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(expectedHabits, result);
        _mockRepository.Verify(r => r.GetAllHabits(), Times.Once);
    }

    [Fact]
    public async Task GetAllHabitsAsync_WhenNoHabits_ReturnsEmptyList()
    {
        // Arrange
        _mockRepository
            .Setup(r => r.GetAllHabits())
            .ReturnsAsync(new List<HabitDto>());

        // Act
        var result = await _service.GetAllHabitsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockRepository.Verify(r => r.GetAllHabits(), Times.Once);
    }

    [Fact]
    public async Task CreateHabitAsync_ValidDto_CreatesHabit()
    {
        // Arrange
        var createDto = new CreateHabitDto
        {
            Name = "New Habit",
            Category = "Health"
        };

        var createdHabit = new Habit
        {
            Id = 1,
            Name = createDto.Name,
            Category = createDto.Category,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _mockRepository
            .Setup(r => r.CreateHabit(createDto))
            .ReturnsAsync(createdHabit);

        // Act
        var result = await _service.CreateHabitAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createDto.Name, result.Name);
        Assert.Equal(createDto.Category, result.Category);
        Assert.True(result.IsActive);
        _mockRepository.Verify(r => r.CreateHabit(createDto), Times.Once);
    }

    [Fact]
    public async Task CreateHabitAsync_WithEmptyName_StillCallsRepository()
    {
        // Arrange
        var createDto = new CreateHabitDto
        {
            Name = "",
            Category = "Test"
        };

        var createdHabit = new Habit
        {
            Id = 1,
            Name = createDto.Name,
            Category = createDto.Category,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _mockRepository
            .Setup(r => r.CreateHabit(createDto))
            .ReturnsAsync(createdHabit);

        // Act
        var result = await _service.CreateHabitAsync(createDto);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify(r => r.CreateHabit(createDto), Times.Once);
    }

    [Fact]
    public async Task DeleteHabitAsync_ValidId_CallsRepositoryDelete()
    {
        // Arrange
        int habitId = 1;
        _mockRepository
            .Setup(r => r.DeleteHabit(habitId))
            .Returns(Task.CompletedTask);

        // Act
        await _service.DeleteHabitAsync(habitId);

        // Assert
        _mockRepository.Verify(r => r.DeleteHabit(habitId), Times.Once);
    }

    [Fact]
    public async Task DeleteHabitAsync_NonExistentId_StillCallsRepository()
    {
        // Arrange
        int habitId = 999;
        _mockRepository
            .Setup(r => r.DeleteHabit(habitId))
            .Returns(Task.CompletedTask);

        // Act
        await _service.DeleteHabitAsync(habitId);

        // Assert
        _mockRepository.Verify(r => r.DeleteHabit(habitId), Times.Once);
    }

    [Fact]
    public async Task MarkHabitCompleteAsync_ValidId_CallsRepositoryMarkComplete()
    {
        // Arrange
        int habitId = 1;
        _mockRepository
            .Setup(r => r.MarkHabitComplete(habitId))
            .Returns(Task.CompletedTask);

        // Act
        await _service.MarkHabitCompleteAsync(habitId);

        // Assert
        _mockRepository.Verify(r => r.MarkHabitComplete(habitId), Times.Once);
    }

    [Fact]
    public async Task MarkHabitCompleteAsync_CalledMultipleTimes_CallsRepositoryEachTime()
    {
        // Arrange
        int habitId = 1;
        _mockRepository
            .Setup(r => r.MarkHabitComplete(habitId))
            .Returns(Task.CompletedTask);

        // Act
        await _service.MarkHabitCompleteAsync(habitId);
        await _service.MarkHabitCompleteAsync(habitId);

        // Assert
        _mockRepository.Verify(r => r.MarkHabitComplete(habitId), Times.Exactly(2));
    }

    [Fact]
    public async Task GetAllHabitsAsync_RepositoryThrowsException_PropagatesException()
    {
        // Arrange
        _mockRepository
            .Setup(r => r.GetAllHabits())
            .ThrowsAsync(new InvalidOperationException("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await _service.GetAllHabitsAsync());
    }

    [Fact]
    public async Task CreateHabitAsync_RepositoryThrowsException_PropagatesException()
    {
        // Arrange
        var createDto = new CreateHabitDto { Name = "Test", Category = "Test" };
        _mockRepository
            .Setup(r => r.CreateHabit(createDto))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await _service.CreateHabitAsync(createDto));
    }
}
