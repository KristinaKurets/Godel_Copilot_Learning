# ?? HabitTracker - Full Stack Application

A complete habit tracking application with .NET 10 backend and React frontend, featuring streak calculation, gamification, and a beautiful pink-themed UI.

## ? Features

- ?? Track daily habits with automatic streak counting
- ?? **Achievement system** with 12 unlockable badges
- ?? **Dashboard statistics** (total habits, active streaks, completions)
- ?? **User profile** with level progression
- ?? **Mock authentication** system
- ? Add/Delete habits with categories
- ? Mark habits complete for today (with duplicate prevention)
- ?? Beautiful pink gradient React UI
- ?? SQLite database with seed data
- ?? RESTful API with Swagger
- ? **69 unit tests** (all passing)
- ??? Clean Architecture backend
- ?? Responsive design (mobile-friendly)

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
  - `Enums/AchievementType.cs` - Achievement type definitions

**2. HabitTracker.Application**
- **Purpose**: Business logic, interfaces, DTOs, and services
- **Dependencies**: HabitTracker.Domain
- **Contains**:
  - **DTOs**: HabitDto, CreateHabitDto, StatisticsDto, AchievementDto, UserProfileDto, LoginRequestDto, LoginResponseDto, CurrentUserDto
  - **Interfaces**: IHabitRepository, IHabitService, IStreakCalculator, IStatisticsService, IAchievementService, IUserProfileService, IAuthService
  - **Services**: HabitService, StreakCalculator

**3. HabitTracker.Infrastructure**
- **Purpose**: Data access with Entity Framework Core and service implementations
- **Dependencies**: Domain, Application, EF Core, SQLite
- **Contains**:
  - `Data/ApplicationDbContext.cs` - EF Core DbContext with seed data
  - `Repositories/HabitRepository.cs` - Repository implementation
  - **Services**: StatisticsService, AchievementService, UserProfileService, MockAuthService

**4. HabitTracker.API**
- **Purpose**: Web API with REST endpoints
- **Dependencies**: Application, Infrastructure, Swagger
- **Contains**:
  - **Controllers**: HabitsController, StatisticsController, AchievementsController, ProfileController, AuthController
  - `Program.cs` - DI, CORS, database initialization

**5. HabitTracker.Tests**
- **Purpose**: Unit tests for business logic
- **Dependencies**: Application, Domain, Infrastructure, xUnit, Moq, EF Core InMemory
- **Contains**:
  - `StreakCalculatorTests.cs` - 13 tests for streak logic
  - `HabitServiceTests.cs` - 11 tests for service layer
  - `StatisticsServiceTests.cs` - 9 tests for statistics
  - `AchievementServiceTests.cs` - 12 tests for achievements
  - `UserProfileServiceTests.cs` - 11 tests for profile
  - `MockAuthServiceTests.cs` - 15 tests for authentication
- **Results**: 69/69 passing ?

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

### Habits Management
| Method | Endpoint | Description | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| GET | `/api/habits` | Get all active habits | - | `HabitDto[]` |
| POST | `/api/habits` | Create new habit | `CreateHabitDto` | `int` (habit id) |
| POST | `/api/habits/{id}/complete` | Mark complete today | - | 204 No Content |
| DELETE | `/api/habits/{id}` | Delete habit | - | 204 No Content |

### Statistics & Gamification
| Method | Endpoint | Description | Response |
|--------|----------|-------------|----------|
| GET | `/api/statistics` | Get dashboard statistics | `StatisticsDto` |
| GET | `/api/achievements` | Get all achievements with progress | `AchievementDto[]` |
| GET | `/api/profile` | Get user profile with level | `UserProfileDto` |

### Authentication (Mock)
| Method | Endpoint | Description | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| POST | `/api/auth/login` | Login with username | `LoginRequestDto` | `LoginResponseDto` |
| GET | `/api/auth/current-user` | Get current logged-in user | - | `CurrentUserDto` |
| POST | `/api/auth/logout` | Logout current user | - | Success message |

### DTOs Examples

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

**StatisticsDto** (Response):
```json
{
  "totalHabits": 4,
  "habitsWithActiveStreaks": 3,
  "completionsThisWeek": 15,
  "completionsThisMonth": 27,
  "longestStreak": 7
}
```

**AchievementDto** (Response):
```json
{
  "id": "SevenDayStreak",
  "name": "Week Warrior",
  "description": "Complete a habit for 7 days in a row",
  "icon": "fire",
  "isUnlocked": true,
  "currentProgress": 7,
  "requiredProgress": 7
}
```

**UserProfileDto** (Response):
```json
{
  "username": "Habit Hero",
  "memberSince": "2024-01-15T10:30:00Z",
  "totalCompletions": 127,
  "achievementsUnlocked": 5,
  "level": 12
}
```

**LoginResponseDto** (Response):
```json
{
  "username": "JohnDoe",
  "token": "mock_token_a1b2c3d4e5f6g7h8i9j0",
  "message": "Login successful"
}
```

---

## ?? Gamification Features

### ?? 12 Achievements
1. **Getting Started** ?? - Create your first habit
2. **Habit Builder** ? - Have 5 active habits at once
3. **Habit Master** ?? - Have 10 active habits at once
4. **Week Warrior** ?? - Complete a habit for 7 days in a row
5. **Perfect Ten** ?? - Complete a habit for 10 days in a row
6. **Monthly Champion** ?? - Complete a habit for 30 days in a row
7. **Unstoppable** ?? - Complete a habit for 60 days in a row
8. **Legendary** ??? - Complete a habit for 90 days in a row
9. **Century Club** ?? - Reach 100 total completions
10. **Dedication** ? - Reach 250 total completions
11. **Epic Achievement** ? - Reach 500 total completions
12. **Perfect Week** ?? - Complete all habits every day for 7 days

### ?? Level System
- **Formula**: Level = Total Completions ÷ 10
- **Examples**:
  - 0-9 completions = Level 0
  - 10-19 completions = Level 1
  - 100 completions = Level 10
  - 500+ completions = Level 50+

### ?? Dashboard Statistics
- Total active habits count
- Habits with current active streaks
- Total completions this week
- Total completions this month
- Longest streak across all habits

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
- ? StatisticsService - 9 tests (dashboard data calculations)
- ? AchievementService - 12 tests (achievement unlock logic)
- ? UserProfileService - 11 tests (profile data aggregation)
- ? MockAuthService - 15 tests (authentication flow)
- ? **Total: 69/69 passing**

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
- ?? Responsive design (mobile + desktop)
- ? Loading states during API calls
- ? Error handling with user-friendly messages
- ?? Confirmation dialogs for deletions
- ?? Auto-refresh after actions

---

## ?? Development

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
- **`API_TESTING_GUIDE.md`** - API testing examples
- **`TEST_DOCUMENTATION.md`** - Unit test documentation
- **`DATABASE_SEED_INFO.md`** - Database schema and seed data

---

## ??? Architecture Highlights

### Backend Patterns
- ?? Clean Architecture (Domain-Application-Infrastructure)
- ?? Repository Pattern
- ?? Dependency Injection
- ?? DTOs for API contracts
- ? Async/await throughout
- ? Unit tested business logic
- ?? Gamification services

### Frontend Patterns
- ?? Single component (simple for learning)
- ?? React Hooks (useState, useEffect)
- ?? Axios for HTTP calls
- ? Error handling
- ? Loading states
- ?? Conditional rendering

---

## ?? Next Steps

### For Learning
1. ?? Run the app and use it
2. ?? Make small changes (colors, text)
3. ? Add a new field (description)
4. ?? Split React component into smaller parts
5. ?? Add new feature (edit habit)
6. ?? Add more achievements
7. ?? Integrate real authentication

### For Production
1. Add real JWT authentication
2. Add input validation
3. Add proper error logging
4. Add integration tests
5. Set up CI/CD pipeline
6. Deploy to cloud
7. Add user database
8. Add password hashing

---

## ?? Contributing

Feel free to:
- Add new features
- Improve UI/UX
- Add more tests
- Refactor code
- Update documentation
- Add more achievements
- Enhance gamification

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
- [x] Statistics endpoint working
- [x] 12 achievements available
- [x] Profile endpoint showing user data
- [x] Mock login/logout working
- [x] 69 unit tests passing
- [x] No console errors

---

## ??? Tech Stack Summary

| Layer | Technology | Version |
|-------|------------|---------|
| Backend Framework | .NET | 10.0 |
| Database | SQLite | - |
| ORM | Entity Framework Core | 10.0.2 |
| Testing | xUnit + Moq + EF InMemory | Latest |
| Frontend Framework | React | 18.3.1 |
| Build Tool | Vite | 6.0.1 |
| HTTP Client | Axios | 1.6.0 |

---

## ?? You're All Set!

Everything is configured and ready to use. Enjoy building your habit tracking app with gamification!

**Questions?** Check the documentation files listed above.

**Happy Coding!** ???

