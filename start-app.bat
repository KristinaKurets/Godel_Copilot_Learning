@echo off
echo ========================================
echo Starting HabitTracker Application
echo ========================================
echo.

echo This will start both the API and UI servers.
echo Press Ctrl+C in each window to stop the servers.
echo.

echo Starting .NET API on http://localhost:5081...
start "HabitTracker API" cmd /k "dotnet run --project HabitTracker.API"

timeout /t 5 /nobreak > nul

echo.
echo Starting React UI on http://localhost:3000...
start "HabitTracker UI" cmd /k "cd habit-tracker-ui && npm run dev"

echo.
echo ========================================
echo Both servers are starting!
echo ========================================
echo.
echo API: http://localhost:5081/swagger
echo UI:  http://localhost:3000
echo.
echo Close this window when done.
pause
