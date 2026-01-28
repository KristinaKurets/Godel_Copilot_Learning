using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Data;
using HabitTracker.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HabitTracker.Tests;

public class StatisticsServiceTests
{
    private ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetStatisticsAsync_WithNoHabits_ReturnsZeroStatistics()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        var service = new StatisticsService(context, mockStreakCalculator.Object);

        // Act
        var result = await service.GetStatisticsAsync();

        // Assert
        Assert.Equal(0, result.TotalHabits);
        Assert.Equal(0, result.HabitsWithActiveStreaks);
        Assert.Equal(0, result.CompletionsThisWeek);
        Assert.Equal(0, result.CompletionsThisMonth);
        Assert.Equal(0, result.LongestStreak);
    }

    [Fact]
    public async Task GetStatisticsAsync_WithHabits_ReturnsTotalHabitsCount()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(0);

        var service = new StatisticsService(context, mockStreakCalculator.Object);

        context.Habits.AddRange(
            new Habit { Id = 1, Name = "Habit 1", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Habit { Id = 2, Name = "Habit 2", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Habit { Id = 3, Name = "Habit 3", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetStatisticsAsync();

        // Assert
        Assert.Equal(3, result.TotalHabits);
    }

    [Fact]
    public async Task GetStatisticsAsync_OnlyCountsActiveHabits()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(0);

        var service = new StatisticsService(context, mockStreakCalculator.Object);

        context.Habits.AddRange(
            new Habit { Id = 1, Name = "Active 1", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Habit { Id = 2, Name = "Inactive", Category = "Test", IsActive = false, CreatedAt = DateTime.UtcNow },
            new Habit { Id = 3, Name = "Active 2", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetStatisticsAsync();

        // Assert
        Assert.Equal(2, result.TotalHabits);
    }

    [Fact]
    public async Task GetStatisticsAsync_CountsHabitsWithActiveStreaks()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();

        var service = new StatisticsService(context, mockStreakCalculator.Object);

        context.Habits.AddRange(
            new Habit { Id = 1, Name = "Habit 1", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Habit { Id = 2, Name = "Habit 2", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Habit { Id = 3, Name = "Habit 3", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();

        mockStreakCalculator
            .SetupSequence(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(5)  // Habit 1 has active streak
            .Returns(0)  // Habit 2 has no streak
            .Returns(3); // Habit 3 has active streak

        // Act
        var result = await service.GetStatisticsAsync();

        // Assert
        Assert.Equal(2, result.HabitsWithActiveStreaks);
    }

    [Fact]
    public async Task GetStatisticsAsync_CalculatesLongestStreak()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();

        var service = new StatisticsService(context, mockStreakCalculator.Object);

        context.Habits.AddRange(
            new Habit { Id = 1, Name = "Habit 1", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Habit { Id = 2, Name = "Habit 2", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Habit { Id = 3, Name = "Habit 3", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();

        mockStreakCalculator
            .SetupSequence(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(5)
            .Returns(12)  // Longest streak
            .Returns(3);

        // Act
        var result = await service.GetStatisticsAsync();

        // Assert
        Assert.Equal(12, result.LongestStreak);
    }

    [Fact]
    public async Task GetStatisticsAsync_CountsCompletionsThisWeek()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(0);

        var service = new StatisticsService(context, mockStreakCalculator.Object);

        var habit = new Habit { Id = 1, Name = "Habit 1", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow };
        context.Habits.Add(habit);
        await context.SaveChangesAsync();

        var today = DateTime.UtcNow.Date;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek);

        context.HabitCompletions.AddRange(
            new HabitCompletion { Id = 1, HabitId = 1, CompletedDate = today },
            new HabitCompletion { Id = 2, HabitId = 1, CompletedDate = today.AddDays(-1) },
            new HabitCompletion { Id = 3, HabitId = 1, CompletedDate = today.AddDays(-2) },
            new HabitCompletion { Id = 4, HabitId = 1, CompletedDate = startOfWeek.AddDays(-1) } // Last week
        );
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetStatisticsAsync();

        // Assert
        Assert.Equal(3, result.CompletionsThisWeek);
    }

    [Fact]
    public async Task GetStatisticsAsync_CountsCompletionsThisMonth()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(0);

        var service = new StatisticsService(context, mockStreakCalculator.Object);

        var habit = new Habit { Id = 1, Name = "Habit 1", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow };
        context.Habits.Add(habit);
        await context.SaveChangesAsync();

        var today = DateTime.UtcNow.Date;
        var startOfMonth = new DateTime(today.Year, today.Month, 1);

        context.HabitCompletions.AddRange(
            new HabitCompletion { Id = 1, HabitId = 1, CompletedDate = today },
            new HabitCompletion { Id = 2, HabitId = 1, CompletedDate = today.AddDays(-5) },
            new HabitCompletion { Id = 3, HabitId = 1, CompletedDate = today.AddDays(-10) },
            new HabitCompletion { Id = 4, HabitId = 1, CompletedDate = startOfMonth },
            new HabitCompletion { Id = 5, HabitId = 1, CompletedDate = startOfMonth.AddDays(-1) } // Last month
        );
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetStatisticsAsync();

        // Assert
        Assert.Equal(4, result.CompletionsThisMonth);
    }

    [Fact]
    public async Task GetStatisticsAsync_WithCompleteData_ReturnsAllStatistics()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();

        var service = new StatisticsService(context, mockStreakCalculator.Object);

        var today = DateTime.UtcNow.Date;

        context.Habits.AddRange(
            new Habit { Id = 1, Name = "Habit 1", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Habit { Id = 2, Name = "Habit 2", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();

        context.HabitCompletions.AddRange(
            new HabitCompletion { Id = 1, HabitId = 1, CompletedDate = today },
            new HabitCompletion { Id = 2, HabitId = 1, CompletedDate = today.AddDays(-1) },
            new HabitCompletion { Id = 3, HabitId = 2, CompletedDate = today }
        );
        await context.SaveChangesAsync();

        mockStreakCalculator
            .SetupSequence(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(7)
            .Returns(5);

        // Act
        var result = await service.GetStatisticsAsync();

        // Assert
        Assert.Equal(2, result.TotalHabits);
        Assert.Equal(2, result.HabitsWithActiveStreaks);
        Assert.Equal(7, result.LongestStreak);
        Assert.True(result.CompletionsThisWeek >= 3);
        Assert.True(result.CompletionsThisMonth >= 3);
    }
}
