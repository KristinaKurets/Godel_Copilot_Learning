@echo off
echo ========================================
echo HabitTracker - Connection Test
echo ========================================
echo.

echo Testing if API is running on http://localhost:5081...
echo.

curl -s http://localhost:5081/api/habits > nul 2>&1
if %errorlevel% equ 0 (
    echo [OK] API is responding on http://localhost:5081
    echo.
    echo Testing API endpoint:
    curl http://localhost:5081/api/habits
    echo.
) else (
    echo [ERROR] API is NOT running on http://localhost:5081
    echo.
    echo Please start the API first:
    echo   dotnet run --project HabitTracker.API
    echo.
)

echo.
echo ========================================
echo.
echo Checking React UI on http://localhost:3000...
echo.

curl -s http://localhost:3000 > nul 2>&1
if %errorlevel% equ 0 (
    echo [OK] React UI is responding on http://localhost:3000
) else (
    echo [ERROR] React UI is NOT running on http://localhost:3000
    echo.
    echo Please start the UI:
    echo   cd habit-tracker-ui
    echo   npm run dev
)

echo.
echo ========================================
echo Summary
echo ========================================
echo.
echo Expected URLs:
echo   API:     http://localhost:5081
echo   Swagger: http://localhost:5081/swagger
echo   UI:      http://localhost:3000
echo.
pause
