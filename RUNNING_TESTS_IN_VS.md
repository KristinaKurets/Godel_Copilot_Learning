# Running Tests in Visual Studio

## Test Explorer

### Opening Test Explorer
1. **Menu**: `Test` ? `Test Explorer`
2. **Keyboard**: `Ctrl+E, T`

### Running Tests
- **Run All Tests**: Click the "Run All" button (??) or `Ctrl+R, A`
- **Run Failed Tests**: Click the "Run Failed Tests" button
- **Run Selected Test**: Right-click test ? "Run"
- **Debug Test**: Right-click test ? "Debug"

## Test Results

After running, you should see:
```
? StreakCalculatorTests (13 passed)
  ? CalculateCurrentStreak_NullCompletionDates_ReturnsZero
  ? CalculateCurrentStreak_EmptyCompletionDates_ReturnsZero
  ? CalculateCurrentStreak_CompletedTodayOnly_ReturnsOne
  ? CalculateCurrentStreak_CompletedYesterdayOnly_ReturnsOne
  ? CalculateCurrentStreak_CompletedTodayAndYesterday_ReturnsTwo
  ? CalculateCurrentStreak_SevenConsecutiveDaysIncludingToday_ReturnsSeven
  ? CalculateCurrentStreak_FiveConsecutiveDaysFromYesterday_ReturnsFive
  ? CalculateCurrentStreak_LastCompletedTwoDaysAgo_ReturnsZero
  ? CalculateCurrentStreak_GapInStreak_CountsOnlyConsecutiveDays
  ? CalculateCurrentStreak_DuplicateDates_CountsAsOne
  ? CalculateCurrentStreak_UnorderedDates_StillCalculatesCorrectly
  ? CalculateCurrentStreak_OnlyOldCompletions_ReturnsZero
  ? CalculateCurrentStreak_DateTimeWithTime_UsesDateOnly

? HabitServiceTests (11 passed)
  ? GetAllHabitsAsync_ReturnsAllHabits
  ? GetAllHabitsAsync_WhenNoHabits_ReturnsEmptyList
  ? CreateHabitAsync_ValidDto_CreatesHabit
  ? CreateHabitAsync_WithEmptyName_StillCallsRepository
  ? DeleteHabitAsync_ValidId_CallsRepositoryDelete
  ? DeleteHabitAsync_NonExistentId_StillCallsRepository
  ? MarkHabitCompleteAsync_ValidId_CallsRepositoryMarkComplete
  ? MarkHabitCompleteAsync_CalledMultipleTimes_CallsRepositoryEachTime
  ? GetAllHabitsAsync_RepositoryThrowsException_PropagatesException
  ? CreateHabitAsync_RepositoryThrowsException_PropagatesException

? StatisticsServiceTests (9 passed)
  ? GetStatisticsAsync_WithNoHabits_ReturnsZeroStatistics
  ? GetStatisticsAsync_WithHabits_ReturnsTotalHabitsCount
  ? GetStatisticsAsync_OnlyCountsActiveHabits
  ? GetStatisticsAsync_CountsHabitsWithActiveStreaks
  ? GetStatisticsAsync_CalculatesLongestStreak
  ? GetStatisticsAsync_CountsCompletionsThisWeek
  ? GetStatisticsAsync_CountsCompletionsThisMonth
  ? GetStatisticsAsync_WithCompleteData_ReturnsAllStatistics

? AchievementServiceTests (12 passed)
  ? GetAchievementsAsync_WithNoHabits_ReturnsAllAchievementsUnlocked
  ? GetAchievementsAsync_WithFirstHabit_UnlocksFirstHabitAchievement
  ? GetAchievementsAsync_WithFiveHabits_UnlocksFiveHabitsAchievement
  ? GetAchievementsAsync_WithSevenDayStreak_UnlocksSevenDayAchievement
  ? GetAchievementsAsync_WithThirtyDayStreak_UnlocksThirtyDayAchievement
  ? GetAchievementsAsync_WithHundredCompletions_UnlocksHundredCompletionsAchievement
  ? GetAchievementsAsync_WithPerfectWeek_UnlocksPerfectWeekAchievement
  ? GetAchievementsAsync_WithPartialProgress_ShowsCorrectProgress
  ? GetAchievementsAsync_ReturnsUnlockedAchievementsFirst
  ? GetAchievementsAsync_OnlyCountsActiveHabits
  ? GetAchievementsAsync_IncludesAllDefinedAchievements
  ? GetAchievementsAsync_AllAchievementsHaveRequiredProperties

? UserProfileServiceTests (11 passed)
  ? GetUserProfileAsync_WithNoData_ReturnsDefaultProfile
  ? GetUserProfileAsync_WithFirstHabit_SetsMemberSinceDate
  ? GetUserProfileAsync_WithMultipleHabits_ReturnsFirstCreationDate
  ? GetUserProfileAsync_IncludesInactiveHabitsForMemberSince
  ? GetUserProfileAsync_CountsTotalCompletions
  ? GetUserProfileAsync_CountsUnlockedAchievements
  ? GetUserProfileAsync_CalculatesLevelFrom10Completions
  ? GetUserProfileAsync_LevelZeroWith9Completions
  ? GetUserProfileAsync_Level10With100Completions
  ? GetUserProfileAsync_AlwaysReturnsHabitHeroUsername
  ? GetUserProfileAsync_WithCompleteProfile_ReturnsAllData

? MockAuthServiceTests (15 passed)
  ? Login_WithValidUsername_ReturnsSuccessWithToken
  ? Login_WithEmptyUsername_ReturnsError
  ? Login_WithWhitespaceUsername_ReturnsError
  ? Login_GeneratesUniqueTokensForSameUser
  ? GetCurrentUser_WithValidToken_ReturnsAuthenticatedUser
  ? GetCurrentUser_WithInvalidToken_ReturnsUnauthenticated
  ? GetCurrentUser_WithNullToken_ReturnsUnauthenticated
  ? GetCurrentUser_WithEmptyToken_ReturnsUnauthenticated
  ? Logout_WithValidToken_RemovesToken
  ? Logout_WithInvalidToken_DoesNotThrow
  ? Logout_WithNullToken_DoesNotThrow
  ? Login_MultipleUsers_StoredIndependently
  ? Logout_OneUser_DoesNotAffectOtherUsers
  ? Login_WithSpecialCharactersInUsername_WorksCorrectly
  ? Login_WithLongUsername_WorksCorrectly

Total: 69 tests, 69 passed ?
```

## Test Suites Overview

| Test Suite | Tests | Purpose |
|------------|-------|---------|
| StreakCalculatorTests | 13 | Streak calculation logic |
| HabitServiceTests | 11 | Habit CRUD operations |
| StatisticsServiceTests | 9 | Dashboard statistics |
| AchievementServiceTests | 12 | Achievement unlocking |
| UserProfileServiceTests | 11 | User profile aggregation |
| MockAuthServiceTests | 15 | Authentication flow |
| **Total** | **69** | **Complete coverage** |

## Debugging Tests

### Set Breakpoints
1. Open the test file
2. Click in the left margin to set a breakpoint
3. Right-click the test ? "Debug"
4. Step through code with F10/F11

### View Test Output
1. Click on a test in Test Explorer
2. View output in the "Test Detail Summary" pane
3. See any console output or assertion messages

## Grouping Tests

### Group by Class
- Click "Group By Class" button in Test Explorer
- Shows tests organized by class

### Group by Outcome
- Shows passed/failed/not run tests separately

### Filter Tests
- Use the search box to filter tests by name
- Example: Type "Streak" to see only streak calculator tests
- Example: Type "Achievement" to see only achievement tests

## Code Coverage (Visual Studio Enterprise)

1. **Run with Coverage**: `Test` ? `Analyze Code Coverage for All Tests`
2. View results in "Code Coverage Results" window
3. See percentage of code covered by tests

## Live Unit Testing (Visual Studio Enterprise)

1. **Enable**: `Test` ? `Live Unit Testing` ? `Start`
2. See real-time indicators in code editor:
   - ? Green checkmark: Covered by passing test
   - ? Red X: Covered by failing test
   - ? Blue dash: Not covered

## Tips

### Quick Test Creation
- Put cursor in method name
- Press `Ctrl+.` ? "Create Unit Tests"
- Visual Studio generates test template

### Test Shortcuts
- `Ctrl+R, T` - Run tests in current context
- `Ctrl+R, Ctrl+T` - Debug tests in current context
- `Ctrl+R, A` - Run all tests
- `Ctrl+R, F` - Run failed tests

### Continuous Testing
- Tests run automatically when you build (if configured)
- Enable in: `Test` ? `Test Explorer` ? Settings icon ? "Run Tests After Build"

## Troubleshooting

### Tests not appearing?
1. Build the solution: `Ctrl+Shift+B`
2. Clean and rebuild: `Build` ? `Clean Solution`, then `Build` ? `Rebuild Solution`
3. Restart Visual Studio

### Tests failing unexpectedly?
1. Check test output for error messages
2. Verify Moq package is installed
3. Verify EF Core InMemory package is installed
4. Ensure project references are correct
5. Check that test project targets correct framework (net10.0)

## Running from Command Line (within VS)

### Package Manager Console
```powershell
dotnet test
```

### Developer Command Prompt
1. `Tools` ? `Command Line` ? `Developer Command Prompt`
2. Run: `dotnet test HabitTracker.Tests\HabitTracker.Tests.csproj`

### Run Specific Test Class
```powershell
dotnet test --filter ClassName=AchievementServiceTests
dotnet test --filter ClassName=MockAuthServiceTests
```

## Best Practices

? **Run tests frequently** - After each code change
? **Keep tests fast** - All 69 tests should run in < 3 seconds
? **Maintain test coverage** - Aim for 80%+ of business logic
? **Fix broken tests immediately** - Don't commit with failing tests
? **Use descriptive test names** - Should explain what's being tested
? **Test edge cases** - Null values, empty collections, boundary conditions
? **Isolate tests** - Use in-memory database with unique instance per test
? **Verify behavior, not implementation** - Test outcomes, not internal details
