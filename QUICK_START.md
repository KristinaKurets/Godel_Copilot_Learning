# ?? Quick Start - Single Page Guide

## ? Your Frontend IS Running!

The dev server started successfully. The URL is correct: **http://localhost:3000**

---

## ?? What To Do Now

### 1. Open Your Browser
Type this URL in your browser: **http://localhost:3000**

### 2. What You'll See

**If working**: Pink themed UI with "?? Habit Tracker ??" header

**If seeing error**: "Failed to load habits" - This means you need to start the API (see step 3)

### 3. Start the .NET API (In a NEW Terminal Window)

**IMPORTANT**: Don't close the current terminal where `npm run dev` is running!

**Open a second terminal** and run:
```bash
dotnet run --project HabitTracker.API
```

### 4. Refresh Browser
Press F5 in your browser at http://localhost:3000

You should now see 4 sample habits! ??

---

## ?? Two Servers = Two Terminals

You need BOTH running at the same time:

| Server | Terminal Command | URL | Status |
|--------|------------------|-----|--------|
| React UI | `npm run dev` | http://localhost:3000 | ? Running |
| .NET API | `dotnet run --project HabitTracker.API` | https://localhost:7037 | ?? Need to start |

---

## ?? If You See a Blank Page

1. Press **F12** (opens DevTools)
2. Click **Console** tab
3. Look for error messages (red text)
4. Check the **Network** tab - are there failed requests?

Most likely: API is not running. Start it in a separate terminal!

---

## ? Success Checklist

When everything works, you'll have:
- [x] Terminal 1: Shows "Local: http://localhost:3000/" (React)
- [ ] Terminal 2: Shows "Now listening on: https://localhost:7037" (API)
- [ ] Browser: Shows pink UI with 4 habits
- [ ] No red errors in browser console (F12)

---

## ?? Quick Test

Once both servers are running:

1. Go to http://localhost:3000
2. Type "Test Habit" in name field
3. Type "Testing" in category field
4. Click "+ Add Habit"
5. Should see it appear in the list below! ?

---

## ?? Remember

- **Keep both terminal windows open** while using the app
- **Ctrl+C** to stop a server
- **F5** to refresh browser
- **F12** to open DevTools and see errors

That's it! Simple as that! ????
