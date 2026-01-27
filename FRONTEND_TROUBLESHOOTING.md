# ?? Frontend Not Loading - Quick Fix Guide

## Problem: Can't see anything on http://localhost:3000

This means the React dev server is **not running yet**.

---

## ? Solution - Start the Dev Server

### Option 1: Use the Batch Script (Easiest)

**Double-click**: `start-app.bat` (in the root folder)

This will:
1. Start the .NET API in one window
2. Start the React UI in another window
3. Both servers will be running

**Then go to**: http://localhost:3000

---

### Option 2: Start Manually

**Open a new terminal** and run:

```bash
cd habit-tracker-ui
npm run dev
```

You should see output like:

```
  VITE v6.0.1  ready in 500 ms

  ?  Local:   http://localhost:3000/
  ?  Network: use --host to expose
  ?  press h + enter to show help
```

**Now go to**: http://localhost:3000

---

## ?? Verify It's Working

### 1. Check the Terminal Output

You should see:
```
  VITE v6.0.1  ready in XXX ms
  ?  Local:   http://localhost:3000/
```

If you see this, the server is running! ?

### 2. Check the Browser

Go to: **http://localhost:3000**

You should see:
- ?? Habit Tracker header
- Pink gradient background
- Add New Habit form
- List of 4 sample habits (if API is running)

### 3. If You See "Failed to load habits"

This means:
- ? React UI is working
- ? API is not running

**Fix**: Start the API in another terminal:
```bash
dotnet run --project HabitTracker.API
```

---

## ?? Common Issues

### Issue 1: "npm: command not found"

**Cause**: Node.js not installed

**Fix**:
1. Download Node.js from https://nodejs.org/
2. Install the LTS version
3. Restart your terminal
4. Try again: `npm run dev`

---

### Issue 2: "Cannot find module 'vite'"

**Cause**: Dependencies not installed

**Fix**:
```bash
cd habit-tracker-ui
npm install
```

Wait for installation to complete, then:
```bash
npm run dev
```

---

### Issue 3: Port 3000 Already in Use

**Error message**:
```
Port 3000 is in use, trying another one...
```

**This is OK!** Vite will use another port (like 3001).

Look for this line in the output:
```
?  Local:   http://localhost:3001/
```

Use **that URL** instead!

---

### Issue 4: Browser Shows "This site can't be reached"

**Cause**: Dev server is not running

**Fix**: 
1. Check terminal - is it running `npm run dev`?
2. If not, start it:
```bash
cd habit-tracker-ui
npm run dev
```

---

### Issue 5: Blank White Page

**Cause**: JavaScript error

**Fix**:
1. Press `F12` to open DevTools
2. Click "Console" tab
3. Look for red error messages
4. Share the error message if you need help

---

## ? Step-by-Step Verification

Follow these steps in order:

### Step 1: Check Node.js Installation
```bash
node --version
```

Should show: `v18.x.x` or higher

If not installed:
1. Download from https://nodejs.org/
2. Install and restart terminal

---

### Step 2: Navigate to UI Folder
```bash
cd habit-tracker-ui
```

You should be in: `C:\Users\k.kurets\source\repos\habit-tracker-ui`

---

### Step 3: Install Dependencies (if not done)
```bash
npm install
```

Wait for it to complete (1-2 minutes first time).

Should end with: "added XXX packages"

---

### Step 4: Start Dev Server
```bash
npm run dev
```

Should show:
```
  VITE v6.0.1  ready in 500 ms
  ?  Local:   http://localhost:3000/
```

**Keep this terminal open!** Don't close it.

---

### Step 5: Open Browser

Go to: **http://localhost:3000**

You should see the pink-themed Habit Tracker UI.

---

### Step 6: Start API (in a different terminal)
```bash
dotnet run --project HabitTracker.API
```

Should show:
```
Database initialized successfully with seed data.
Now listening on: https://localhost:7037
```

---

### Step 7: Refresh Browser

Go back to: http://localhost:3000

You should now see:
- 4 sample habits from seed data
- Each with streak counters
- Complete and Delete buttons

---

## ?? Expected Result

When everything is working:

1. **Terminal 1 (API)**: Shows "Now listening on: https://localhost:7037"
2. **Terminal 2 (UI)**: Shows "Local: http://localhost:3000/"
3. **Browser**: Shows pink UI with 4 habits

---

## ?? Still Not Working?

### Try This:

1. **Close all terminals**

2. **Open PowerShell in the repository root**

3. **Run these commands one by one**:

```powershell
# Check if dependencies are installed
cd habit-tracker-ui
npm install

# Start the dev server
npm run dev
```

4. **Look for the URL in the output**:
```
?  Local:   http://localhost:XXXX/
```

5. **Use that URL** (might be 3000, 3001, or another port)

---

## ?? Quick Debug Commands

```bash
# Check Node.js
node --version

# Check npm
npm --version

# Check if in correct folder
pwd
# Should show: .../habit-tracker-ui

# List files
dir
# Should see: package.json, vite.config.js, src folder

# Install dependencies
npm install

# Start dev server
npm run dev
```

---

## ?? What Each Command Does

| Command | What It Does |
|---------|--------------|
| `npm install` | Downloads all packages (like `dotnet restore`) |
| `npm run dev` | Starts Vite dev server (like `dotnet run`) |
| `http://localhost:3000` | Where your React app runs |
| `https://localhost:7037` | Where your .NET API runs |

---

## ?? Success Looks Like This

**Terminal Output:**
```
  VITE v6.0.1  ready in 500 ms

  ?  Local:   http://localhost:3000/
  ?  Network: use --host to expose
```

**Browser (http://localhost:3000):**
- Pink gradient background ?
- "?? Habit Tracker ??" header ?
- Add New Habit form ?
- List of habits below ?

---

## ?? Next Steps If Still Stuck

1. **Screenshot the terminal output** after running `npm run dev`
2. **Screenshot the browser** showing what you see (or don't see)
3. **Check browser console** (F12) for error messages
4. **Share any error messages** you see

The most common issue is simply **forgetting to run the dev server**. Make sure `npm run dev` is running in a terminal!

---

## ? Quick Checklist

Before saying "it's not working":

- [ ] Node.js installed (`node --version` works)
- [ ] In correct folder (`cd habit-tracker-ui`)
- [ ] Dependencies installed (`npm install` completed)
- [ ] Dev server running (`npm run dev` is active)
- [ ] Terminal shows "Local: http://localhost:XXXX"
- [ ] Using the URL shown in terminal
- [ ] Terminal window is still open (don't close it!)

If all checked, it WILL work! ??
