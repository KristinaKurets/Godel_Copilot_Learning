# HabitTracker API Test Examples

## Swagger UI (Recommended)

The easiest way to test is through Swagger UI:
1. Run the application
2. Navigate to `https://localhost:7037/swagger`
3. Try out all endpoints directly from the browser with interactive documentation

---

## ?? Authentication Endpoints

### 1. Login (Mock Authentication)
```bash
curl -X POST "https://localhost:7037/api/auth/login" \
  -H "Content-Type: application/json" \
  -d "{\"username\":\"HabitHero\"}"
```

**PowerShell:**
```powershell
$body = @{ username = "HabitHero" } | ConvertTo-Json
Invoke-RestMethod -Uri "https://localhost:7037/api/auth/login" -Method Post -Body $body -ContentType "application/json"
```

**Response:**
```json
{
  "username": "HabitHero",
  "token": "mock_token_a1b2c3d4e5f6g7h8i9j0",
  "message": "Login successful"
}
```

### 2. Get Current User
```bash
curl -X GET "https://localhost:7037/api/auth/current-user" \
  -H "Authorization: Bearer mock_token_a1b2c3d4e5f6g7h8i9j0"
```

**PowerShell:**
```powershell
$headers = @{ Authorization = "Bearer mock_token_a1b2c3d4e5f6g7h8i9j0" }
Invoke-RestMethod -Uri "https://localhost:7037/api/auth/current-user" -Method Get -Headers $headers
```

### 3. Logout
```bash
curl -X POST "https://localhost:7037/api/auth/logout" \
  -H "Authorization: Bearer mock_token_a1b2c3d4e5f6g7h8i9j0"
```

---

## ?? Habit Management Endpoints

### 1. Get All Habits (with streak information)
```bash
curl -X GET "https://localhost:7037/api/habits" -H "accept: application/json"
```

**PowerShell:**
```powershell
Invoke-RestMethod -Uri "https://localhost:7037/api/habits" -Method Get
```

**Expected response:**
```json
[
  {
    "id": 1,
    "name": "Morning Exercise",
    "category": "Fitness",
    "currentStreak": 7,
    "totalCompletions": 7
  },
  {
    "id": 2,
    "name": "Read Books",
    "category": "Learning",
    "currentStreak": 5,
    "totalCompletions": 5
  },
  {
    "id": 3,
    "name": "Drink Water",
    "category": "Health",
    "currentStreak": 0,
    "totalCompletions": 8
  },
  {
    "id": 4,
    "name": "Meditation",
    "category": "Wellness",
    "currentStreak": 3,
    "totalCompletions": 3
  }
]
```

### 2. Create a New Habit
```bash
curl -X POST "https://localhost:7037/api/habits" \
  -H "Content-Type: application/json" \
  -d "{\"name\":\"Daily Coding\",\"category\":\"Learning\"}"
```

**PowerShell:**
```powershell
$body = @{
    name = "Daily Coding"
    category = "Learning"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7037/api/habits" -Method Post -Body $body -ContentType "application/json"
```

### 3. Mark Habit Complete for Today
```bash
curl -X POST "https://localhost:7037/api/habits/1/complete" \
  -H "accept: */*"
```

**PowerShell:**
```powershell
Invoke-RestMethod -Uri "https://localhost:7037/api/habits/1/complete" -Method Post
```

### 4. Delete a Habit
```bash
curl -X DELETE "https://localhost:7037/api/habits/5" \
  -H "accept: */*"
```

**PowerShell:**
```powershell
Invoke-RestMethod -Uri "https://localhost:7037/api/habits/5" -Method Delete
```

---

## ?? Statistics Endpoint

### Get Dashboard Statistics
```bash
curl -X GET "https://localhost:7037/api/statistics" -H "accept: application/json"
```

**PowerShell:**
```powershell
Invoke-RestMethod -Uri "https://localhost:7037/api/statistics" -Method Get
```

**Expected response:**
```json
{
  "totalHabits": 4,
  "habitsWithActiveStreaks": 3,
  "completionsThisWeek": 15,
  "completionsThisMonth": 27,
  "longestStreak": 7
}
```

---

## ?? Achievements Endpoint

### Get All Achievements
```bash
curl -X GET "https://localhost:7037/api/achievements" -H "accept: application/json"
```

**PowerShell:**
```powershell
Invoke-RestMethod -Uri "https://localhost:7037/api/achievements" -Method Get
```

**Expected response:**
```json
[
  {
    "id": "FirstHabit",
    "name": "Getting Started",
    "description": "Create your first habit",
    "icon": "seedling",
    "isUnlocked": true,
    "currentProgress": 1,
    "requiredProgress": 1
  },
  {
    "id": "SevenDayStreak",
    "name": "Week Warrior",
    "description": "Complete a habit for 7 days in a row",
    "icon": "fire",
    "isUnlocked": true,
    "currentProgress": 7,
    "requiredProgress": 7
  },
  {
    "id": "ThirtyDayStreak",
    "name": "Monthly Champion",
    "description": "Complete a habit for 30 days in a row",
    "icon": "trophy",
    "isUnlocked": false,
    "currentProgress": 7,
    "requiredProgress": 30
  }
]
```

---

## ?? User Profile Endpoint

### Get User Profile
```bash
curl -X GET "https://localhost:7037/api/profile" -H "accept: application/json"
```

**PowerShell:**
```powershell
Invoke-RestMethod -Uri "https://localhost:7037/api/profile" -Method Get
```

**Expected response:**
```json
{
  "username": "Habit Hero",
  "memberSince": "2024-01-15T10:30:00Z",
  "totalCompletions": 127,
  "achievementsUnlocked": 5,
  "level": 12
}
```

---

## ?? Testing Scenarios

### 1. Test Active Streak
- Call `GET /api/habits`
- Verify "Morning Exercise" has `currentStreak: 7`

### 2. Test Broken Streak
- Verify "Drink Water" has `currentStreak: 0` (last completed 3 days ago)

### 3. Test Marking Complete
1. Mark "Drink Water" complete: `POST /api/habits/3/complete`
2. Get all habits again: `GET /api/habits`
3. Verify "Drink Water" now has `currentStreak: 1`

### 4. Test Duplicate Prevention
1. Mark same habit complete twice in one day
2. Should not create duplicate completions
3. Streak should remain the same

### 5. Test Achievement Unlock
1. Create your first habit: `POST /api/habits`
2. Check achievements: `GET /api/achievements`
3. Verify "Getting Started" is unlocked

### 6. Test Level Progression
1. Check current level: `GET /api/profile`
2. Complete 10 habits
3. Check level again - should increase by 1

### 7. Test Statistics Update
1. Get initial statistics: `GET /api/statistics`
2. Complete a habit: `POST /api/habits/1/complete`
3. Get statistics again - completions should increase

### 8. Test Authentication Flow
1. Login: `POST /api/auth/login`
2. Save the token from response
3. Use token in subsequent requests
4. Check current user: `GET /api/auth/current-user`
5. Logout: `POST /api/auth/logout`
6. Token should now be invalid

---

## ?? Complete Testing Workflow

```powershell
# 1. Login
$loginResponse = Invoke-RestMethod -Uri "https://localhost:7037/api/auth/login" `
    -Method Post -Body '{"username":"TestUser"}' -ContentType "application/json"
$token = $loginResponse.token

# 2. Get Profile
$headers = @{ Authorization = "Bearer $token" }
Invoke-RestMethod -Uri "https://localhost:7037/api/profile" -Method Get -Headers $headers

# 3. Get Statistics
Invoke-RestMethod -Uri "https://localhost:7037/api/statistics" -Method Get

# 4. Get Achievements
Invoke-RestMethod -Uri "https://localhost:7037/api/achievements" -Method Get

# 5. Create New Habit
$habitBody = @{
    name = "Test Habit"
    category = "Testing"
} | ConvertTo-Json
Invoke-RestMethod -Uri "https://localhost:7037/api/habits" -Method Post -Body $habitBody -ContentType "application/json"

# 6. Get All Habits
Invoke-RestMethod -Uri "https://localhost:7037/api/habits" -Method Get

# 7. Logout
Invoke-RestMethod -Uri "https://localhost:7037/api/auth/logout" -Method Post -Headers $headers
```

---

## ?? API Endpoints Summary

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/auth/login` | POST | Login with username |
| `/api/auth/current-user` | GET | Get current user |
| `/api/auth/logout` | POST | Logout |
| `/api/habits` | GET | Get all habits |
| `/api/habits` | POST | Create habit |
| `/api/habits/{id}/complete` | POST | Mark complete |
| `/api/habits/{id}` | DELETE | Delete habit |
| `/api/statistics` | GET | Get dashboard stats |
| `/api/achievements` | GET | Get achievements |
| `/api/profile` | GET | Get user profile |
