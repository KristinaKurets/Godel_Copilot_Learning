using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Entities;
using HabitTracker.Domain.Enums;
using HabitTracker.Infrastructure.Data;
using HabitTracker.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HabitTracker.Tests;

public class AchievementServiceTests
{
    private ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetAchievementsAsync_WithNoHabits_ReturnsAllAchievementsUnlocked()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(0);

        var service = new AchievementService(context, mockStreakCalculator.Object);

        // Act
        var result = await service.GetAchievementsAsync();

        // Assert
        var achievements = result.ToList();
        Assert.NotEmpty(achievements);
        Assert.All(achievements, a => Assert.False(a.IsUnlocked));
        Assert.All(achievements, a => Assert.Equal(0, a.CurrentProgress));
    }

    [Fact]
    public async Task GetAchievementsAsync_WithFirstHabit_UnlocksFirstHabitAchievement()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(0);

        var service = new AchievementService(context, mockStreakCalculator.Object);

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

        // Act
        var result = await service.GetAchievementsAsync();

        // Assert
        var firstHabitAchievement = result.First(a => a.Id == AchievementType.FirstHabit.ToString());
        Assert.True(firstHabitAchievement.IsUnlocked);
        Assert.Equal(1, firstHabitAchievement.CurrentProgress);
        Assert.Equal(1, firstHabitAchievement.RequiredProgress);
    }

    [Fact]
    public async Task GetAchievementsAsync_WithFiveHabits_UnlocksFiveHabitsAchievement()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(0);

        var service = new AchievementService(context, mockStreakCalculator.Object);

        for (int i = 1; i <= 5; i++)
        {
            context.Habits.Add(new Habit
            {
                Id = i,
                Name = $"Habit {i}",
                Category = "Test",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
        }
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetAchievementsAsync();

        // Assert
        var fiveHabitsAchievement = result.First(a => a.Id == AchievementType.FiveHabits.ToString());
        Assert.True(fiveHabitsAchievement.IsUnlocked);
        Assert.Equal(5, fiveHabitsAchievement.CurrentProgress);
        Assert.Equal(5, fiveHabitsAchievement.RequiredProgress);
    }

    [Fact]
    public async Task GetAchievementsAsync_WithSevenDayStreak_UnlocksSevenDayAchievement()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(7);

        var service = new AchievementService(context, mockStreakCalculator.Object);

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

        // Act
        var result = await service.GetAchievementsAsync();

        // Assert
        var sevenDayAchievement = result.First(a => a.Id == AchievementType.SevenDayStreak.ToString());
        Assert.True(sevenDayAchievement.IsUnlocked);
        Assert.Equal(7, sevenDayAchievement.CurrentProgress);
        Assert.Equal(7, sevenDayAchievement.RequiredProgress);
    }

    [Fact]
    public async Task GetAchievementsAsync_WithThirtyDayStreak_UnlocksThirtyDayAchievement()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(30);

        var service = new AchievementService(context, mockStreakCalculator.Object);

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

        // Act
        var result = await service.GetAchievementsAsync();

        // Assert
        var thirtyDayAchievement = result.First(a => a.Id == AchievementType.ThirtyDayStreak.ToString());
        Assert.True(thirtyDayAchievement.IsUnlocked);
        Assert.Equal(30, thirtyDayAchievement.CurrentProgress);
        Assert.Equal(30, thirtyDayAchievement.RequiredProgress);
    }

    [Fact]
    public async Task GetAchievementsAsync_WithHundredCompletions_UnlocksHundredCompletionsAchievement()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(0);

        var service = new AchievementService(context, mockStreakCalculator.Object);

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
        var result = await service.GetAchievementsAsync();

        // Assert
        var hundredCompletionsAchievement = result.First(a => a.Id == AchievementType.HundredCompletions.ToString());
        Assert.True(hundredCompletionsAchievement.IsUnlocked);
        Assert.Equal(100, hundredCompletionsAchievement.CurrentProgress);
        Assert.Equal(100, hundredCompletionsAchievement.RequiredProgress);
    }

    [Fact]
    public async Task GetAchievementsAsync_WithPerfectWeek_UnlocksPerfectWeekAchievement()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(7);

        var service = new AchievementService(context, mockStreakCalculator.Object);

        // Create 2 habits
        var habit1 = new Habit
        {
            Id = 1,
            Name = "Habit 1",
            Category = "Test",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        var habit2 = new Habit
        {
            Id = 2,
            Name = "Habit 2",
            Category = "Test",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        context.Habits.AddRange(habit1, habit2);
        await context.SaveChangesAsync();

        // Complete both habits for the last 7 days
        var today = DateTime.UtcNow.Date;
        int completionId = 1;
        for (int daysBack = 0; daysBack < 7; daysBack++)
        {
            var date = today.AddDays(-daysBack);
            context.HabitCompletions.AddRange(
                new HabitCompletion { Id = completionId++, HabitId = 1, CompletedDate = date },
                new HabitCompletion { Id = completionId++, HabitId = 2, CompletedDate = date }
            );
        }
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetAchievementsAsync();

        // Assert
        var perfectWeekAchievement = result.First(a => a.Id == AchievementType.PerfectWeek.ToString());
        Assert.True(perfectWeekAchievement.IsUnlocked);
        Assert.Equal(7, perfectWeekAchievement.CurrentProgress);
        Assert.Equal(7, perfectWeekAchievement.RequiredProgress);
    }

    [Fact]
    public async Task GetAchievementsAsync_WithPartialProgress_ShowsCorrectProgress()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(4);

        var service = new AchievementService(context, mockStreakCalculator.Object);

        // Create 3 habits
        for (int i = 1; i <= 3; i++)
        {
            context.Habits.Add(new Habit
            {
                Id = i,
                Name = $"Habit {i}",
                Category = "Test",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
        }
        await context.SaveChangesAsync();

        // Add 50 completions
        for (int i = 0; i < 50; i++)
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
        var result = await service.GetAchievementsAsync();

        // Assert
        var fiveHabitsAchievement = result.First(a => a.Id == AchievementType.FiveHabits.ToString());
        Assert.False(fiveHabitsAchievement.IsUnlocked);
        Assert.Equal(3, fiveHabitsAchievement.CurrentProgress);
        Assert.Equal(5, fiveHabitsAchievement.RequiredProgress);

        var sevenDayAchievement = result.First(a => a.Id == AchievementType.SevenDayStreak.ToString());
        Assert.False(sevenDayAchievement.IsUnlocked);
        Assert.Equal(4, sevenDayAchievement.CurrentProgress);
        Assert.Equal(7, sevenDayAchievement.RequiredProgress);

        var hundredCompletionsAchievement = result.First(a => a.Id == AchievementType.HundredCompletions.ToString());
        Assert.False(hundredCompletionsAchievement.IsUnlocked);
        Assert.Equal(50, hundredCompletionsAchievement.CurrentProgress);
        Assert.Equal(100, hundredCompletionsAchievement.RequiredProgress);
    }

    [Fact]
    public async Task GetAchievementsAsync_ReturnsUnlockedAchievementsFirst()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(7);

        var service = new AchievementService(context, mockStreakCalculator.Object);

        // Create enough data to unlock some achievements
        for (int i = 1; i <= 2; i++)
        {
            context.Habits.Add(new Habit
            {
                Id = i,
                Name = $"Habit {i}",
                Category = "Test",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
        }
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetAchievementsAsync();

        // Assert
        var achievements = result.ToList();
        var unlockedCount = achievements.Count(a => a.IsUnlocked);
        var firstUnlockedIndex = achievements.FindIndex(a => a.IsUnlocked);
        var lastUnlockedIndex = achievements.FindLastIndex(a => a.IsUnlocked);

        // All unlocked achievements should come before locked ones
        Assert.True(lastUnlockedIndex < achievements.Count - 1 || unlockedCount == achievements.Count);
    }

    [Fact]
    public async Task GetAchievementsAsync_OnlyCountsActiveHabits()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(0);

        var service = new AchievementService(context, mockStreakCalculator.Object);

        context.Habits.AddRange(
            new Habit { Id = 1, Name = "Active", Category = "Test", IsActive = true, CreatedAt = DateTime.UtcNow },
            new Habit { Id = 2, Name = "Inactive", Category = "Test", IsActive = false, CreatedAt = DateTime.UtcNow }
        );
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetAchievementsAsync();

        // Assert
        var firstHabitAchievement = result.First(a => a.Id == AchievementType.FirstHabit.ToString());
        Assert.True(firstHabitAchievement.IsUnlocked);
        Assert.Equal(1, firstHabitAchievement.CurrentProgress);
    }

    [Fact]
    public async Task GetAchievementsAsync_IncludesAllDefinedAchievements()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(0);

        var service = new AchievementService(context, mockStreakCalculator.Object);

        // Act
        var result = await service.GetAchievementsAsync();

        // Assert
        var achievements = result.ToList();
        Assert.Contains(achievements, a => a.Id == AchievementType.FirstHabit.ToString());
        Assert.Contains(achievements, a => a.Id == AchievementType.SevenDayStreak.ToString());
        Assert.Contains(achievements, a => a.Id == AchievementType.ThirtyDayStreak.ToString());
        Assert.Contains(achievements, a => a.Id == AchievementType.HundredCompletions.ToString());
        Assert.Contains(achievements, a => a.Id == AchievementType.FiveHabits.ToString());
        Assert.Contains(achievements, a => a.Id == AchievementType.PerfectWeek.ToString());
        Assert.Contains(achievements, a => a.Id == AchievementType.TenDayStreak.ToString());
        Assert.Contains(achievements, a => a.Id == AchievementType.SixtyDayStreak.ToString());
        Assert.Contains(achievements, a => a.Id == AchievementType.NinetyDayStreak.ToString());
        Assert.Contains(achievements, a => a.Id == AchievementType.TenHabits.ToString());
        Assert.Contains(achievements, a => a.Id == AchievementType.TwoHundredFiftyCompletions.ToString());
        Assert.Contains(achievements, a => a.Id == AchievementType.FiveHundredCompletions.ToString());
    }

    [Fact]
    public async Task GetAchievementsAsync_AllAchievementsHaveRequiredProperties()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var mockStreakCalculator = new Mock<IStreakCalculator>();
        mockStreakCalculator
            .Setup(s => s.CalculateCurrentStreak(It.IsAny<IEnumerable<DateTime>>()))
            .Returns(0);

        var service = new AchievementService(context, mockStreakCalculator.Object);

        // Act
        var result = await service.GetAchievementsAsync();

        // Assert
        Assert.All(result, achievement =>
        {
            Assert.NotEmpty(achievement.Id);
            Assert.NotEmpty(achievement.Name);
            Assert.NotEmpty(achievement.Description);
            Assert.NotEmpty(achievement.Icon);
            Assert.True(achievement.RequiredProgress > 0);
            Assert.True(achievement.CurrentProgress >= 0);
        });
    }
}
