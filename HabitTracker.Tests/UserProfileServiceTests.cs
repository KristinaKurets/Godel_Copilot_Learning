using HabitTracker.Application.DTOs;
using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Data;
using HabitTracker.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HabitTracker.Tests;

public class UserProfileServiceTests
{
    private ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetUserProfileAsync_WithNoData_ReturnsDefaultProfile()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockAchievementService = new Mock<IAchievementService>();
        mockAchievementService
            .Setup(s => s.GetAchievementsAsync())
            .ReturnsAsync(new List<AchievementDto>());

        var service = new UserProfileService(context, mockAchievementService.Object);

        // Act
        var result = await service.GetUserProfileAsync();

        // Assert
        Assert.Equal("Habit Hero", result.Username);
        Assert.Null(result.MemberSince);
        Assert.Equal(0, result.TotalCompletions);
        Assert.Equal(0, result.AchievementsUnlocked);
        Assert.Equal(0, result.Level);
    }

    [Fact]
    public async Task GetUserProfileAsync_WithFirstHabit_SetsMemberSinceDate()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockAchievementService = new Mock<IAchievementService>();
        mockAchievementService
            .Setup(s => s.GetAchievementsAsync())
            .ReturnsAsync(new List<AchievementDto>());

        var service = new UserProfileService(context, mockAchievementService.Object);

        var firstHabitDate = new DateTime(2024, 1, 1);
        context.Habits.Add(new Habit
        {
            Id = 1,
            Name = "First Habit",
            Category = "Test",
            IsActive = true,
            CreatedAt = firstHabitDate
        });
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetUserProfileAsync();

        // Assert
        Assert.Equal(firstHabitDate, result.MemberSince);
    }

    [Fact]
    public async Task GetUserProfileAsync_WithMultipleHabits_ReturnsFrstCreationDate()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockAchievementService = new Mock<IAchievementService>();
        mockAchievementService
            .Setup(s => s.GetAchievementsAsync())
            .ReturnsAsync(new List<AchievementDto>());

        var service = new UserProfileService(context, mockAchievementService.Object);

        var firstDate = new DateTime(2024, 1, 1);
        var secondDate = new DateTime(2024, 1, 15);
        var thirdDate = new DateTime(2024, 2, 1);

        context.Habits.AddRange(
            new Habit { Id = 1, Name = "Habit 1", Category = "Test", IsActive = true, CreatedAt = secondDate },
            new Habit { Id = 2, Name = "Habit 2", Category = "Test", IsActive = true, CreatedAt = firstDate },
            new Habit { Id = 3, Name = "Habit 3", Category = "Test", IsActive = true, CreatedAt = thirdDate }
        );
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetUserProfileAsync();

        // Assert
        Assert.Equal(firstDate, result.MemberSince);
    }

    [Fact]
    public async Task GetUserProfileAsync_IncludesInactiveHabitsForMemberSince()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockAchievementService = new Mock<IAchievementService>();
        mockAchievementService
            .Setup(s => s.GetAchievementsAsync())
            .ReturnsAsync(new List<AchievementDto>());

        var service = new UserProfileService(context, mockAchievementService.Object);

        var oldDate = new DateTime(2023, 1, 1);
        var newDate = new DateTime(2024, 1, 1);

        context.Habits.AddRange(
            new Habit { Id = 1, Name = "Old Habit", Category = "Test", IsActive = false, CreatedAt = oldDate },
            new Habit { Id = 2, Name = "New Habit", Category = "Test", IsActive = true, CreatedAt = newDate }
        );
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetUserProfileAsync();

        // Assert
        Assert.Equal(oldDate, result.MemberSince);
    }

    [Fact]
    public async Task GetUserProfileAsync_CountsTotalCompletions()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockAchievementService = new Mock<IAchievementService>();
        mockAchievementService
            .Setup(s => s.GetAchievementsAsync())
            .ReturnsAsync(new List<AchievementDto>());

        var service = new UserProfileService(context, mockAchievementService.Object);

        var habit = new Habit
        {
            Id = 1,
            Name = "Test Habit",
            Category = "Test",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        context.Habits.Add(habit);
        await context.SaveChangesAsync();

        // Add 25 completions
        for (int i = 0; i < 25; i++)
        {
            context.HabitCompletions.Add(new HabitCompletion
            {
                Id = i + 1,
                HabitId = 1,
                CompletedDate = DateTime.UtcNow.AddDays(-i)
            });
        }
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetUserProfileAsync();

        // Assert
        Assert.Equal(25, result.TotalCompletions);
    }

    [Fact]
    public async Task GetUserProfileAsync_CountsUnlockedAchievements()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockAchievementService = new Mock<IAchievementService>();

        var achievements = new List<AchievementDto>
        {
            new AchievementDto { Id = "1", Name = "Achievement 1", IsUnlocked = true, Icon = "star", Description = "Test", CurrentProgress = 1, RequiredProgress = 1 },
            new AchievementDto { Id = "2", Name = "Achievement 2", IsUnlocked = true, Icon = "star", Description = "Test", CurrentProgress = 1, RequiredProgress = 1 },
            new AchievementDto { Id = "3", Name = "Achievement 3", IsUnlocked = false, Icon = "star", Description = "Test", CurrentProgress = 0, RequiredProgress = 1 },
            new AchievementDto { Id = "4", Name = "Achievement 4", IsUnlocked = true, Icon = "star", Description = "Test", CurrentProgress = 1, RequiredProgress = 1 }
        };

        mockAchievementService
            .Setup(s => s.GetAchievementsAsync())
            .ReturnsAsync(achievements);

        var service = new UserProfileService(context, mockAchievementService.Object);

        // Act
        var result = await service.GetUserProfileAsync();

        // Assert
        Assert.Equal(3, result.AchievementsUnlocked);
    }

    [Fact]
    public async Task GetUserProfileAsync_CalculatesLevelFrom10Completions()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockAchievementService = new Mock<IAchievementService>();
        mockAchievementService
            .Setup(s => s.GetAchievementsAsync())
            .ReturnsAsync(new List<AchievementDto>());

        var service = new UserProfileService(context, mockAchievementService.Object);

        var habit = new Habit
        {
            Id = 1,
            Name = "Test Habit",
            Category = "Test",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        context.Habits.Add(habit);
        await context.SaveChangesAsync();

        // Add 35 completions (should result in level 3)
        for (int i = 0; i < 35; i++)
        {
            context.HabitCompletions.Add(new HabitCompletion
            {
                Id = i + 1,
                HabitId = 1,
                CompletedDate = DateTime.UtcNow.AddDays(-i)
            });
        }
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetUserProfileAsync();

        // Assert
        Assert.Equal(3, result.Level);
        Assert.Equal(35, result.TotalCompletions);
    }

    [Fact]
    public async Task GetUserProfileAsync_LevelZeroWith9Completions()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockAchievementService = new Mock<IAchievementService>();
        mockAchievementService
            .Setup(s => s.GetAchievementsAsync())
            .ReturnsAsync(new List<AchievementDto>());

        var service = new UserProfileService(context, mockAchievementService.Object);

        var habit = new Habit
        {
            Id = 1,
            Name = "Test Habit",
            Category = "Test",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        context.Habits.Add(habit);
        await context.SaveChangesAsync();

        // Add 9 completions
        for (int i = 0; i < 9; i++)
        {
            context.HabitCompletions.Add(new HabitCompletion
            {
                Id = i + 1,
                HabitId = 1,
                CompletedDate = DateTime.UtcNow.AddDays(-i)
            });
        }
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetUserProfileAsync();

        // Assert
        Assert.Equal(0, result.Level);
    }

    [Fact]
    public async Task GetUserProfileAsync_Level10With100Completions()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockAchievementService = new Mock<IAchievementService>();
        mockAchievementService
            .Setup(s => s.GetAchievementsAsync())
            .ReturnsAsync(new List<AchievementDto>());

        var service = new UserProfileService(context, mockAchievementService.Object);

        var habit = new Habit
        {
            Id = 1,
            Name = "Test Habit",
            Category = "Test",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        context.Habits.Add(habit);
        await context.SaveChangesAsync();

        // Add 100 completions
        for (int i = 0; i < 100; i++)
        {
            context.HabitCompletions.Add(new HabitCompletion
            {
                Id = i + 1,
                HabitId = 1,
                CompletedDate = DateTime.UtcNow.AddDays(-i)
            });
        }
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetUserProfileAsync();

        // Assert
        Assert.Equal(10, result.Level);
    }

    [Fact]
    public async Task GetUserProfileAsync_AlwaysReturnsHabitHeroUsername()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockAchievementService = new Mock<IAchievementService>();
        mockAchievementService
            .Setup(s => s.GetAchievementsAsync())
            .ReturnsAsync(new List<AchievementDto>());

        var service = new UserProfileService(context, mockAchievementService.Object);

        // Act
        var result = await service.GetUserProfileAsync();

        // Assert
        Assert.Equal("Habit Hero", result.Username);
    }

    [Fact]
    public async Task GetUserProfileAsync_WithCompleteProfile_ReturnsAllData()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockAchievementService = new Mock<IAchievementService>();

        var achievements = new List<AchievementDto>
        {
            new AchievementDto { Id = "1", Name = "A1", IsUnlocked = true, Icon = "star", Description = "Test", CurrentProgress = 1, RequiredProgress = 1 },
            new AchievementDto { Id = "2", Name = "A2", IsUnlocked = true, Icon = "star", Description = "Test", CurrentProgress = 1, RequiredProgress = 1 },
            new AchievementDto { Id = "3", Name = "A3", IsUnlocked = false, Icon = "star", Description = "Test", CurrentProgress = 0, RequiredProgress = 1 }
        };

        mockAchievementService
            .Setup(s => s.GetAchievementsAsync())
            .ReturnsAsync(achievements);

        var service = new UserProfileService(context, mockAchievementService.Object);

        var habitDate = new DateTime(2024, 1, 1);
        var habit = new Habit
        {
            Id = 1,
            Name = "Test Habit",
            Category = "Test",
            IsActive = true,
            CreatedAt = habitDate
        };
        context.Habits.Add(habit);
        await context.SaveChangesAsync();

        // Add 55 completions
        for (int i = 0; i < 55; i++)
        {
            context.HabitCompletions.Add(new HabitCompletion
            {
                Id = i + 1,
                HabitId = 1,
                CompletedDate = DateTime.UtcNow.AddDays(-i)
            });
        }
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetUserProfileAsync();

        // Assert
        Assert.Equal("Habit Hero", result.Username);
        Assert.Equal(habitDate, result.MemberSince);
        Assert.Equal(55, result.TotalCompletions);
        Assert.Equal(2, result.AchievementsUnlocked);
        Assert.Equal(5, result.Level);
    }
}
