# HabitTracker API Test Examples

## Using cURL

### 1. Get All Habits (with streak information)
```bash
curl -X GET "https://localhost:5001/api/habits" -H "accept: application/json"
```

Expected response:
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
curl -X POST "https://localhost:5001/api/habits" \
  -H "Content-Type: application/json" \
  -d "{\"name\":\"Daily Coding\",\"category\":\"Learning\"}"
```

### 3. Mark Habit Complete for Today
```bash
curl -X POST "https://localhost:5001/api/habits/1/complete" \
  -H "accept: */*"
```

### 4. Delete a Habit
```bash
curl -X DELETE "https://localhost:5001/api/habits/5" \
  -H "accept: */*"
```

## Using PowerShell

### 1. Get All Habits
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/habits" -Method Get
```

### 2. Create a New Habit
```powershell
$body = @{
    name = "Daily Coding"
    category = "Learning"
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/habits" -Method Post -Body $body -ContentType "application/json"
```

### 3. Mark Habit Complete
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/habits/1/complete" -Method Post
```

### 4. Delete a Habit
```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/habits/5" -Method Delete
```

## Testing Streak Calculation

1. **Test active streak**: Call GET /api/habits and verify "Morning Exercise" has currentStreak: 7

2. **Test broken streak**: Verify "Drink Water" has currentStreak: 0 (last completed 3 days ago)

3. **Test marking complete**: 
   - Mark "Drink Water" complete: POST /api/habits/3/complete
   - Get all habits again: GET /api/habits
   - Verify "Drink Water" now has currentStreak: 1

4. **Test duplicate prevention**:
   - Mark same habit complete twice in one day
   - Should not create duplicate completions

## Swagger UI

The easiest way to test is through Swagger UI:
1. Run the application
2. Navigate to `https://localhost:<port>/swagger`
3. Try out the endpoints directly from the browser
