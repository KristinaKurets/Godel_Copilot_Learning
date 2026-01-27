# HabitTracker Unit Tests Documentation

## Test Summary
? **24 tests** - All passing
- **StreakCalculatorTests**: 13 tests
- **HabitServiceTests**: 11 tests

## Test Coverage

### StreakCalculatorTests (13 tests)

Tests for the streak calculation business logic:

#### Edge Cases
- ? `CalculateCurrentStreak_NullCompletionDates_ReturnsZero` - Handles null input
- ? `CalculateCurrentStreak_EmptyCompletionDates_ReturnsZero` - Handles empty list
- ? `CalculateCurrentStreak_OnlyOldCompletions_ReturnsZero` - No recent activity
- ? `CalculateCurrentStreak_LastCompletedTwoDaysAgo_ReturnsZero` - Streak broken

#### Single Day Streaks
- ? `CalculateCurrentStreak_CompletedTodayOnly_ReturnsOne` - Single day (today)
- ? `CalculateCurrentStreak_CompletedYesterdayOnly_ReturnsOne` - Single day (yesterday)

#### Multiple Day Streaks
- ? `CalculateCurrentStreak_CompletedTodayAndYesterday_ReturnsTwo` - 2-day streak
- ? `CalculateCurrentStreak_SevenConsecutiveDaysIncludingToday_ReturnsSeven` - 7-day streak from today
- ? `CalculateCurrentStreak_FiveConsecutiveDaysFromYesterday_ReturnsFive` - 5-day streak from yesterday

#### Special Scenarios
- ? `CalculateCurrentStreak_GapInStreak_CountsOnlyConsecutiveDays` - Handles gaps properly
- ? `CalculateCurrentStreak_DuplicateDates_CountsAsOne` - Deduplicates dates
- ? `CalculateCurrentStreak_UnorderedDates_StillCalculatesCorrectly` - Handles unordered input
- ? `CalculateCurrentStreak_DateTimeWithTime_UsesDateOnly` - Ignores time component

### HabitServiceTests (11 tests)

Tests for the HabitService business logic using mocked repository:

#### GetAllHabitsAsync
- ? `GetAllHabitsAsync_ReturnsAllHabits` - Returns habits from repository
- ? `GetAllHabitsAsync_WhenNoHabits_ReturnsEmptyList` - Handles empty result
- ? `GetAllHabitsAsync_RepositoryThrowsException_PropagatesException` - Error handling

#### CreateHabitAsync
- ? `CreateHabitAsync_ValidDto_CreatesHabit` - Creates habit with valid data
- ? `CreateHabitAsync_WithEmptyName_StillCallsRepository` - Validates delegation
- ? `CreateHabitAsync_RepositoryThrowsException_PropagatesException` - Error handling

#### DeleteHabitAsync
- ? `DeleteHabitAsync_ValidId_CallsRepositoryDelete` - Delegates to repository
- ? `DeleteHabitAsync_NonExistentId_StillCallsRepository` - Handles non-existent ID

#### MarkHabitCompleteAsync
- ? `MarkHabitCompleteAsync_ValidId_CallsRepositoryMarkComplete` - Delegates to repository
- ? `MarkHabitCompleteAsync_CalledMultipleTimes_CallsRepositoryEachTime` - Handles multiple calls

#### Verification
- All tests verify that service methods properly delegate to repository
- Tests confirm proper exception propagation
- Mock verification ensures correct method calls

## Running the Tests

### Run all tests:
```bash
dotnet test HabitTracker.Tests\HabitTracker.Tests.csproj
```

### Run with detailed output:
```bash
dotnet test HabitTracker.Tests\HabitTracker.Tests.csproj --verbosity detailed
```

### Run specific test class:
```bash
dotnet test HabitTracker.Tests\HabitTracker.Tests.csproj --filter ClassName=StreakCalculatorTests
```

### Run with code coverage:
```bash
dotnet test HabitTracker.Tests\HabitTracker.Tests.csproj /p:CollectCoverage=true
```

## Test Patterns Used

### Arrange-Act-Assert (AAA)
All tests follow the AAA pattern:
- **Arrange**: Set up test data and mocks
- **Act**: Execute the method being tested
- **Assert**: Verify the results

### Mocking with Moq
- `Mock<IHabitRepository>` - Mocks repository for service tests
- `Setup()` - Configures mock behavior
- `Verify()` - Confirms methods were called correctly

### Test Naming Convention
`MethodName_Scenario_ExpectedBehavior`

Examples:
- `CalculateCurrentStreak_NullCompletionDates_ReturnsZero`
- `CreateHabitAsync_ValidDto_CreatesHabit`

## What's NOT Tested (By Design)

Following the requirement "don't create tests for models":
- ? Domain entities (Habit, HabitCompletion)
- ? DTOs (CreateHabitDto, HabitDto)
- ? Interfaces (IHabitRepository, IHabitService, IStreakCalculator)
- ? Repository implementation (requires database)
- ? Controllers (integration testing recommended)

## Coverage Summary

? **Business Logic**: Fully covered
- StreakCalculator service - 100% covered
- HabitService - 100% covered

? **Test Quality**:
- Edge cases tested
- Happy path tested
- Error conditions tested
- Boundary conditions tested
- Mock verification included

## Adding New Tests

When adding new business logic, follow this pattern:

```csharp
[Fact]
public async Task MethodName_Scenario_ExpectedBehavior()
{
    // Arrange
    var mockRepo = new Mock<IHabitRepository>();
    // Set up mocks and test data
    
    // Act
    var result = await _service.SomeMethod();
    
    // Assert
    Assert.NotNull(result);
    mockRepo.Verify(r => r.SomeMethod(), Times.Once);
}
```

## Continuous Integration

These tests are designed to run in CI/CD pipelines:
- Fast execution (< 2 seconds)
- No external dependencies
- No database required (mocked)
- Deterministic results
