@echo off
echo ========================================
echo HabitTracker Database Test
echo ========================================
echo.

echo Cleaning up old database...
if exist "HabitTracker.API\habittracker.db" del "HabitTracker.API\habittracker.db"
if exist "HabitTracker.API\habittracker.db-shm" del "HabitTracker.API\habittracker.db-shm"
if exist "HabitTracker.API\habittracker.db-wal" del "HabitTracker.API\habittracker.db-wal"
echo Done.
echo.

echo Building the project...
dotnet build HabitTracker.API\HabitTracker.API.csproj
if %errorlevel% neq 0 (
    echo Build failed!
    pause
    exit /b %errorlevel%
)
echo Build successful.
echo.

echo Starting the API (database will be initialized with seed data)...
echo Press Ctrl+C to stop the server.
echo.
dotnet run --project HabitTracker.API\HabitTracker.API.csproj
