using HabitTracker.Application.Services;

namespace HabitTracker.Tests;

public class StreakCalculatorTests
{
    private readonly StreakCalculator _calculator;

    public StreakCalculatorTests()
    {
        _calculator = new StreakCalculator();
    }

    [Fact]
    public void CalculateCurrentStreak_NullCompletionDates_ReturnsZero()
    {
        // Arrange
        IEnumerable<DateTime>? completionDates = null;

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates!);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateCurrentStreak_EmptyCompletionDates_ReturnsZero()
    {
        // Arrange
        var completionDates = new List<DateTime>();

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateCurrentStreak_CompletedTodayOnly_ReturnsOne()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var completionDates = new List<DateTime> { today };

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalculateCurrentStreak_CompletedYesterdayOnly_ReturnsOne()
    {
        // Arrange
        var yesterday = DateTime.UtcNow.Date.AddDays(-1);
        var completionDates = new List<DateTime> { yesterday };

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalculateCurrentStreak_CompletedTodayAndYesterday_ReturnsTwo()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var yesterday = today.AddDays(-1);
        var completionDates = new List<DateTime> { today, yesterday };

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void CalculateCurrentStreak_SevenConsecutiveDaysIncludingToday_ReturnsSeven()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var completionDates = new List<DateTime>
        {
            today,
            today.AddDays(-1),
            today.AddDays(-2),
            today.AddDays(-3),
            today.AddDays(-4),
            today.AddDays(-5),
            today.AddDays(-6)
        };

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(7, result);
    }

    [Fact]
    public void CalculateCurrentStreak_FiveConsecutiveDaysFromYesterday_ReturnsFive()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var yesterday = today.AddDays(-1);
        var completionDates = new List<DateTime>
        {
            yesterday,
            yesterday.AddDays(-1),
            yesterday.AddDays(-2),
            yesterday.AddDays(-3),
            yesterday.AddDays(-4)
        };

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void CalculateCurrentStreak_LastCompletedTwoDaysAgo_ReturnsZero()
    {
        // Arrange
        var twoDaysAgo = DateTime.UtcNow.Date.AddDays(-2);
        var completionDates = new List<DateTime>
        {
            twoDaysAgo,
            twoDaysAgo.AddDays(-1),
            twoDaysAgo.AddDays(-2)
        };

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateCurrentStreak_GapInStreak_CountsOnlyConsecutiveDays()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var completionDates = new List<DateTime>
        {
            today,
            today.AddDays(-1),
            today.AddDays(-2),
            // Gap here (day -3 missing)
            today.AddDays(-4),
            today.AddDays(-5)
        };

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(3, result); // Only counts today, -1, -2
    }

    [Fact]
    public void CalculateCurrentStreak_DuplicateDates_CountsAsOne()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var completionDates = new List<DateTime>
        {
            today,
            today,
            today,
            today.AddDays(-1),
            today.AddDays(-1)
        };

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void CalculateCurrentStreak_UnorderedDates_StillCalculatesCorrectly()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var completionDates = new List<DateTime>
        {
            today.AddDays(-2),
            today,
            today.AddDays(-1),
            today.AddDays(-3)
        };

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void CalculateCurrentStreak_OnlyOldCompletions_ReturnsZero()
    {
        // Arrange
        var tenDaysAgo = DateTime.UtcNow.Date.AddDays(-10);
        var completionDates = new List<DateTime>
        {
            tenDaysAgo,
            tenDaysAgo.AddDays(-1),
            tenDaysAgo.AddDays(-2)
        };

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateCurrentStreak_DateTimeWithTime_UsesDateOnly()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var completionDates = new List<DateTime>
        {
            today.AddHours(10),
            today.AddDays(-1).AddHours(15),
            today.AddDays(-2).AddHours(8)
        };

        // Act
        var result = _calculator.CalculateCurrentStreak(completionDates);

        // Assert
        Assert.Equal(3, result);
    }
}
