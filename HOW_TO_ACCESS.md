# ?? UPDATED - How to Access Your App

## ? Correct URLs (Updated)

Your application uses these URLs:

| What | URL | Click to Open |
|------|-----|---------------|
| **React Frontend** | http://localhost:3000 | [Open UI](http://localhost:3000) |
| **API Backend** | http://localhost:5081 | [Open API](http://localhost:5081/swagger) |
| **Swagger Docs** | http://localhost:5081/swagger | [Open Swagger](http://localhost:5081/swagger) |

**Note**: Using **HTTP** (not HTTPS) on port **5081** (not 7037)

---

## ?? Step-by-Step to See Your UI

### Step 1: Start the API

**Open Terminal 1:**
```bash
dotnet run --project HabitTracker.API
```

**Wait for this message:**
```
Database initialized successfully with seed data.
Now listening on: http://localhost:5081
```

? **Verify**: Open http://localhost:5081/swagger - should show Swagger UI

---

### Step 2: Start the React UI

**Open Terminal 2 (keep Terminal 1 open!):**
```bash
cd habit-tracker-ui
npm run dev
```

**Wait for this message:**
```
  VITE v6.4.1  ready in 502 ms
  ?  Local:   http://localhost:3000/
```

? **Verify**: Terminal shows "Local: http://localhost:3000/"

---

### Step 3: Open Browser

**Type this URL in your browser:** http://localhost:3000

**You should see:**
- ?? Pink gradient background
- "?? Habit Tracker ??" header
- Add New Habit form
- 4 sample habits:
  - Morning Exercise - ?? 7 day streak
  - Read Books - ?? 5 day streak
  - Drink Water - ?? 0 day streak
  - Meditation - ?? 3 day streak

---

## ? Quick Test

### Test 1: Verify UI Loads
- Go to http://localhost:3000
- Should see pink interface ?

### Test 2: Verify API Connection
- Should see 4 sample habits in the list ?
- If you see "Failed to load habits", API is not running

### Test 3: Create a Habit
- Type "Test" in name field
- Type "Testing" in category field
- Click "+ Add Habit"
- Should appear in the list ?

### Test 4: Complete a Habit
- Click "? Complete Today" on any habit
- Streak should increase ?

---

## ?? If You Don't See Anything

### Check 1: Is React Dev Server Running?

**Look at your terminal** - should show:
```
  ?  Local:   http://localhost:3000/
```

**If NOT showing this:**
```bash
cd habit-tracker-ui
npm run dev
```

### Check 2: Are You Using the Correct URL?

**Use**: http://localhost:3000 (HTTP, port 3000)

**NOT**:
- ? https://localhost:3000 (wrong protocol)
- ? http://localhost:5081 (that's the API)
- ? http://localhost:7037 (old URL)

### Check 3: Check Browser Console

1. Press **F12** to open DevTools
2. Click **Console** tab
3. Look for error messages

**Common errors:**
- "Failed to fetch" ? API is not running
- "Syntax error" ? Check terminal for build errors

### Check 4: Is Terminal Still Open?

The `npm run dev` terminal must stay open while you use the app!

If you closed it, restart:
```bash
cd habit-tracker-ui
npm run dev
```

---

## ?? Both Servers Must Be Running

You need **BOTH** servers at the same time:

```
Terminal 1 (API)              Terminal 2 (UI)
================              ================
dotnet run --project          cd habit-tracker-ui
HabitTracker.API              npm run dev
                    
http://localhost:5081         http://localhost:3000
Keep this open! ?----------?  Keep this open!
```

---

## ?? Visual Guide

```
Browser (http://localhost:3000)
         ?
    React App (Vite dev server on port 3000)
         ?
    Vite Proxy (/api ? http://localhost:5081)
         ?
    .NET API (http://localhost:5081)
         ?
    SQLite Database
```

---

## ? Quick Start Command

**Easiest way** - Just run:
```bash
start-app.bat
```

This opens both servers automatically! ??

**Then open browser to**: http://localhost:3000

---

## ? Success Checklist

You'll know it's working when:
- [ ] Terminal 1 shows: "Now listening on: http://localhost:5081"
- [ ] Terminal 2 shows: "Local: http://localhost:3000/"
- [ ] Browser shows pink UI with habits
- [ ] Can create/complete/delete habits
- [ ] No red errors in browser console (F12)

---

## ?? That's It!

**The URL is**: http://localhost:3000

**Just make sure**:
1. ? React dev server is running (`npm run dev`)
2. ? .NET API is running (`dotnet run --project HabitTracker.API`)
3. ? Both terminals stay open
4. ? You're using **http** (not https)
5. ? Port is **3000** (not 5081)

Everything is now configured for **http://localhost:5081** (API) and **http://localhost:3000** (UI)! ????
