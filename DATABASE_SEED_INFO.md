# HabitTracker - Database Seed Data

## Database Configuration

The application uses **SQLite** for easy testing and development. The database file `habittracker.db` is created automatically in the API project directory on first run.

## Seed Data

The database is automatically seeded with test data on startup. Here's what's included:

### Habits

| ID | Name | Category | Description | Created | Status |
|----|------|----------|-------------|---------|--------|
| 1 | Morning Exercise | Fitness | 30 minutes of morning workout | 30 days ago | Active |
| 2 | Read Books | Learning | Read for at least 20 minutes | 20 days ago | Active |
| 3 | Drink Water | Health | Drink 8 glasses of water | 15 days ago | Active |
| 4 | Meditation | Wellness | 10 minutes of mindfulness meditation | 10 days ago | Active |

### Completion Patterns (for Streak Testing)

1. **Morning Exercise**: 7-day active streak (completed last 7 days including today)
   - Expected Current Streak: 7

2. **Read Books**: 5-day active streak (completed last 5 days including today)
   - Expected Current Streak: 5

3. **Drink Water**: Broken streak (last completed 3 days ago, had 8 days before that)
   - Expected Current Streak: 0

4. **Meditation**: 3-day active streak (completed last 3 days including today)
   - Expected Current Streak: 3

## Testing the Application

1. **Run the API**:
   ```bash
   dotnet run --project HabitTracker.API
   ```

2. **Access Swagger UI**:
   Navigate to `https://localhost:<port>/swagger`

3. **Test Endpoints**:

   - **GET /api/habits** - Get all habits with streak information
   - **POST /api/habits** - Create a new habit
     ```json
     {
       "name": "New Habit",
       "category": "Custom"
     }
     ```
   - **POST /api/habits/{id}/complete** - Mark a habit complete for today
   - **DELETE /api/habits/{id}** - Delete a habit

## Database Reset

To reset the database with fresh seed data:
1. Stop the application
2. Delete the `habittracker.db` file
3. Restart the application - it will recreate and seed the database

## Streak Calculation Logic

The `StreakCalculator` counts consecutive days:
- A streak is "active" if the most recent completion is today or yesterday
- Days must be consecutive (no gaps)
- Only one completion per day is counted

Example: If checked in on Mon, Tue, Wed, Thu, Fri (5 days in a row) = streak of 5
