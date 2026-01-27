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

Total: 24 tests, 24 passed ?
```

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
3. Ensure project references are correct
4. Check that test project targets correct framework (net10.0)

## Running from Command Line (within VS)

### Package Manager Console
```powershell
dotnet test
```

### Developer Command Prompt
1. `Tools` ? `Command Line` ? `Developer Command Prompt`
2. Run: `dotnet test HabitTracker.Tests\HabitTracker.Tests.csproj`

## Best Practices

? **Run tests frequently** - After each code change
? **Keep tests fast** - All tests should run in < 5 seconds
? **Maintain test coverage** - Aim for 80%+ of business logic
? **Fix broken tests immediately** - Don't commit with failing tests
? **Use descriptive test names** - Should explain what's being tested
