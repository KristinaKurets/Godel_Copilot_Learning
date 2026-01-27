# ? Frontend Is Running!

## Server Status: RUNNING ?

The Vite dev server is now running at:

### ?? **http://localhost:3000/**

---

## What You Should See

### In Your Terminal:
```
  VITE v6.4.1  ready in 502 ms

  ?  Local:   http://localhost:3000/
  ?  Network: use --host to expose
```

### In Your Browser (http://localhost:3000):

**If API is Running:**
- ?? Pink gradient background
- "?? Habit Tracker ??" header
- Add New Habit form (2 input fields + button)
- List showing 4 sample habits:
  1. Morning Exercise - ?? 7 day streak
  2. Read Books - ?? 5 day streak
  3. Drink Water - ?? 0 day streak
  4. Meditation - ?? 3 day streak

**If API is NOT Running:**
- ?? Pink gradient background
- "?? Habit Tracker ??" header
- Add New Habit form
- Red error message: "Failed to load habits. Make sure the API is running..."

---

## ?? Next Step: Start the API

**Open a NEW terminal window** (keep the React one open!) and run:

```bash
dotnet run --project HabitTracker.API
```

Wait for:
```
Database initialized successfully with seed data.
Now listening on: https://localhost:7037
```

Then **refresh your browser** (F5) at http://localhost:3000

You should now see all 4 sample habits! ?

---

## ?? Both Servers Running

You need TWO terminal windows:

**Terminal 1 - React UI:**
```
PS C:\...\habit-tracker-ui> npm run dev
  ?  Local:   http://localhost:3000/
```
**Keep this open!**

**Terminal 2 - .NET API:**
```
PS C:\...\repos> dotnet run --project HabitTracker.API
Now listening on: https://localhost:7037
```
**Keep this open too!**

---

## ?? Testing the Full Stack

Now that both servers are running:

### 1. Open Browser
Go to: http://localhost:3000

### 2. You Should See:
- ? Pink themed UI
- ? 4 sample habits with streaks
- ? No error messages

### 3. Test Creating a Habit:
- Type "Daily Coding" in name field
- Type "Learning" in category field
- Click "+ Add Habit"
- ? Should see success message
- ? New habit appears in list

### 4. Test Completing a Habit:
- Click "? Complete Today" on any habit
- ? Should see success message
- ? Streak should increase by 1

### 5. Test Duplicate Prevention:
- Click "? Complete Today" on same habit again
- ? Should see: "?? You already completed this habit today!"

### 6. Test Deleting:
- Click "??? Delete" on a habit
- ? Confirmation popup appears
- Click OK
- ? Habit removed from list

---

## ?? Stopping the Servers

### To Stop React UI:
- Go to the terminal running `npm run dev`
- Press `Ctrl+C`
- Type `Y` if prompted

### To Stop .NET API:
- Go to the terminal running `dotnet run`
- Press `Ctrl+C`

---

## ?? Restarting

### Easy Way:
Close all terminals and double-click: **`start-app.bat`**

### Manual Way:
```bash
# Terminal 1
cd habit-tracker-ui
npm run dev

# Terminal 2
dotnet run --project HabitTracker.API
```

---

## ?? URLs Summary

| What | URL | Status |
|------|-----|--------|
| React UI | http://localhost:3000 | ? Running |
| .NET API | https://localhost:7037 | Start in separate terminal |
| Swagger | https://localhost:7037/swagger | Available when API runs |

---

## ? You're All Set!

The React frontend is now running on **http://localhost:3000**.

**Just make sure**:
1. Keep the terminal open (don't close it)
2. Start the API in another terminal if you haven't
3. Refresh browser if needed

**Enjoy your habit tracker!** ????

---

## ?? Pro Tips

- **Auto-reload**: Save any file in `src/` folder and browser refreshes automatically
- **DevTools**: Press F12 to see console, network requests, and errors
- **Hot Module Reload**: Changes appear instantly (no page refresh needed)
- **Terminal Logs**: Watch the terminal for any error messages

Happy coding! ??
