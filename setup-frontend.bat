@echo off
echo ========================================
echo HabitTracker Frontend Setup
echo ========================================
echo.

echo Checking if Node.js is installed...
node --version >nul 2>&1
if %errorlevel% neq 0 (
    echo [ERROR] Node.js is not installed!
    echo.
    echo Please install Node.js from: https://nodejs.org/
    echo Download the LTS version and install with default options.
    echo.
    pause
    exit /b 1
)

echo [OK] Node.js version:
node --version
echo.

echo Checking if npm is installed...
npm --version >nul 2>&1
if %errorlevel% neq 0 (
    echo [ERROR] npm is not installed!
    pause
    exit /b 1
)

echo [OK] npm version:
npm --version
echo.

echo ========================================
echo Installing frontend dependencies...
echo This will take 1-2 minutes...
echo ========================================
echo.

cd habit-tracker-ui

if not exist "package.json" (
    echo [ERROR] package.json not found!
    echo Make sure you're running this from the repository root.
    pause
    exit /b 1
)

echo Running: npm install
echo.
npm install

if %errorlevel% neq 0 (
    echo.
    echo [ERROR] Installation failed!
    echo.
    echo Try these fixes:
    echo 1. Delete node_modules folder
    echo 2. Run: npm cache clean --force
    echo 3. Try again: npm install
    echo.
    pause
    exit /b 1
)

echo.
echo ========================================
echo [SUCCESS] Installation Complete!
echo ========================================
echo.
echo Frontend setup is complete.
echo.
echo Next steps:
echo 1. Start the API: dotnet run --project HabitTracker.API
echo 2. Start the UI: cd habit-tracker-ui ^&^& npm run dev
echo 3. Open browser: http://localhost:3000
echo.
echo Or simply run: start-app.bat
echo.
pause
