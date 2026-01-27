# ?? HabitTracker - Full Stack Application

A complete habit tracking application with .NET 10 backend and React frontend, featuring streak calculation and a beautiful pink-themed UI.

## ?? Features

- ? Track daily habits with automatic streak counting
- ? Add/Delete habits with categories
- ? Mark habits complete for today (with duplicate prevention)
- ? Beautiful pink gradient React UI
- ? SQLite database with seed data
- ? RESTful API
- ? 24 unit tests (all passing)
- ? Clean Architecture backend
- ? Responsive design (mobile-friendly)

---

## ?? Quick Start

### First Time Setup

**1. Install Node.js** (if not installed):
- Download from https://nodejs.org/ (LTS version)

**2. Run Setup Script:**
```bash
setup-frontend.bat
```

**3. Start Application:**
```bash
start-app.bat
```

**4. Open Browser:**
- **UI**: http://localhost:3000
- **API Swagger**: https://localhost:7037/swagger

### Already Set Up?

Just run: **`start-app.bat`**

---

## ?? Project Structure

### Backend Projects (.NET 10)

**1. HabitTracker.Domain**
- **Purpose**: Domain entities and business rules
- **Dependencies**: None (Core layer)
- **Contains**:
  - `Entities/Habit.cs` - Habit entity with Id, Name, Category, CreatedAt
  - `Entities/HabitCompletion.cs` - Completion tracking with Id, HabitId, CompletedDate

**2. HabitTracker.Application**
- **Purpose**: Business logic, interfaces, DTOs, and services
- **Dependencies**: HabitTracker.Domain
- **Contains**:
  - `DTOs/CreateHabitDto.cs` - API input contract
  - `DTOs/HabitDto.cs` - API response contract with streak info
  - `Interfaces/IHabitRepository.cs` - Repository contract
  - `Interfaces/IHabitService.cs` - Service contract
  - `Interfaces/IStreakCalculator.cs` - Streak calculation contract
  - `Services/HabitService.cs` - Business logic implementation
  - `Services/StreakCalculator.cs` - Streak calculation logic

**3. HabitTracker.Infrastructure**
- **Purpose**: Data access with Entity Framework Core
- **Dependencies**: Domain, Application, EF Core, SQLite
- **Contains**:
  - `Data/ApplicationDbContext.cs` - EF Core DbContext with seed data
  - `Repositories/HabitRepository.cs` - Repository implementation

**4. HabitTracker.API**
- **Purpose**: Web API with REST endpoints
- **Dependencies**: Application, Infrastructure, Swagger
- **Contains**:
  - `Controllers/HabitsController.cs` - REST API endpoints
  - `Program.cs` - DI, CORS, database initialization

**5. HabitTracker.Tests**
- **Purpose**: Unit tests for business logic
- **Dependencies**: Application, Domain, xUnit, Moq
- **Contains**:
  - `StreakCalculatorTests.cs` - 13 tests for streak logic
  - `HabitServiceTests.cs` - 11 tests for service layer
- **Results**: 24/24 passing ?

### Frontend Project (React + Vite)

**habit-tracker-ui/**
- **Purpose**: React frontend with pink theme
- **Technologies**: React 18, Vite 6, Axios
- **Contains**:
  - `src/App.jsx` - Main component with all UI logic
  - `src/App.css` - Pink gradient theme
  - `vite.config.js` - Proxy config for API calls

---

## ?? API Endpoints

| Method | Endpoint | Description | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| GET | `/api/habits` | Get all active habits | - | `HabitDto[]` |
| POST | `/api/habits` | Create new habit | `CreateHabitDto` | `int` (habit id) |
| POST | `/api/habits/{id}/complete` | Mark complete today | - | 204 No Content |
| DELETE | `/api/habits/{id}` | Delete habit | - | 204 No Content |

### DTOs

**CreateHabitDto** (Request):
```json
{
  "name": "Morning Exercise",
  "category": "Fitness"
}
```

**HabitDto** (Response):
```json
{
  "id": 1,
  "name": "Morning Exercise",
  "category": "Fitness",
  "currentStreak": 7,
  "totalCompletions": 15
}
```

---

## ?? Database

- **Type**: SQLite
- **File**: `HabitTracker.API/habittracker.db`
- **Auto-created**: Yes (on first run)

### Seed Data

4 sample habits with different streak patterns:
1. **Morning Exercise** (Fitness) - 7-day active streak
2. **Read Books** (Learning) - 5-day active streak
3. **Drink Water** (Health) - 0 streak (broken 3 days ago)
4. **Meditation** (Wellness) - 3-day active streak

**Reset database**: Delete `habittracker.db` file and restart API

---

## ?? Testing

### Unit Tests (Backend)
```bash
dotnet test HabitTracker.Tests
```

**Coverage**:
- ? StreakCalculator - 13 tests (edge cases, streaks, gaps)
- ? HabitService - 11 tests (CRUD operations, error handling)
- ? Total: 24/24 passing

### Integration Testing (Full Stack)
1. Start both servers
2. Open http://localhost:3000
3. Test all UI operations
4. Verify in Swagger: https://localhost:7037/swagger

---

## ?? UI Screenshots

### Main Interface
- Pink gradient background
- Add Habit form at top
- Habits list with streak counters
- Complete and Delete buttons for each habit
- Success/error message notifications

### Features
- ? Responsive design (mobile + desktop)
- ? Loading states during API calls
- ? Error handling with user-friendly messages
- ? Confirmation dialogs for deletions
- ? Auto-refresh after actions

---

## ??? Development

### Backend
```bash
# Run with auto-reload
dotnet watch --project HabitTracker.API

# Run tests in watch mode
dotnet watch test --project HabitTracker.Tests
```

### Frontend
```bash
cd habit-tracker-ui

# Start dev server (auto-reload)
npm run dev

# Changes auto-reload in browser!
```

---

## ?? Documentation

Comprehensive guides for every aspect:

- **`GETTING_STARTED.md`** - Complete setup walkthrough
- **`FULL_STACK_SETUP.md`** - Full stack development guide
- **`habit-tracker-ui/BACKEND_DEV_GUIDE.md`** - React for backend developers
- **`API_TESTING_GUIDE.md`** - API testing examples
- **`TEST_DOCUMENTATION.md`** - Unit test documentation
- **`DATABASE_SEED_INFO.md`** - Database schema and seed data

---

## ?? Architecture Highlights

### Backend Patterns
- ? Clean Architecture (Domain-Application-Infrastructure)
- ? Repository Pattern
- ? Dependency Injection
- ? DTOs for API contracts
- ? Async/await throughout
- ? Unit tested business logic

### Frontend Patterns
- ? Single component (simple for learning)
- ? React Hooks (useState, useEffect)
- ? Axios for HTTP calls
- ? Error handling
- ? Loading states
- ? Conditional rendering

---

## ?? Next Steps

### For Learning
1. ? Run the app and use it
2. ? Make small changes (colors, text)
3. ? Add a new field (description)
4. ? Split React component into smaller parts
5. ? Add new feature (edit habit)

### For Production
1. Add authentication/authorization
2. Add input validation
3. Add proper error logging
4. Add integration tests
5. Set up CI/CD pipeline
6. Deploy to cloud

---

## ?? Contributing

Feel free to:
- Add new features
- Improve UI/UX
- Add more tests
- Refactor code
- Update documentation

---

## ? Verification Checklist

After setup, you should have:
- [x] .NET API running on https://localhost:7037
- [x] React UI running on http://localhost:3000
- [x] 4 seed habits visible in UI
- [x] Can create new habits
- [x] Can mark habits complete
- [x] Can delete habits
- [x] Streaks calculate correctly
- [x] 24 unit tests passing
- [x] No console errors

---

## ?? Tech Stack Summary

| Layer | Technology | Version |
|-------|------------|---------|
| Backend Framework | .NET | 10.0 |
| Database | SQLite | - |
| ORM | Entity Framework Core | 10.0.2 |
| Testing | xUnit + Moq | Latest |
| Frontend Framework | React | 18.3.1 |
| Build Tool | Vite | 6.0.1 |
| HTTP Client | Axios | 1.6.0 |

---

## ?? You're All Set!

Everything is configured and ready to use. Enjoy building your habit tracking app!

**Questions?** Check the documentation files listed above.

**Happy Coding!** ????

