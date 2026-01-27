# ?? Quick Setup Instructions

## Step 1: Install Node.js (if not already installed)
Download from: https://nodejs.org/
Choose LTS version (recommended for most users)

## Step 2: Install Frontend Dependencies

Open a terminal in the project root and run:

```bash
cd habit-tracker-ui
npm install
```

This will install:
- React 18.3.1
- Axios 1.6.0
- Vite 6.0.1

**Wait for it to complete** (might take 1-2 minutes first time)

## Step 3: Start the Application

### Option A: Use the Batch Script (Windows)
Just double-click: **`start-app.bat`**

This opens two windows:
- ? API Server (https://localhost:7037)
- ? UI Server (http://localhost:3000)

### Option B: Manual Start (Any OS)

**Terminal 1** - Start API:
```bash
dotnet run --project HabitTracker.API
```

**Terminal 2** - Start UI:
```bash
cd habit-tracker-ui
npm run dev
```

## Step 4: Open Browser

Navigate to: **http://localhost:3000**

You should see:
- ?? Pink gradient background
- Form to add new habits
- List of 4 sample habits (from seed data)
- Streak counters showing 7, 5, 0, and 3 days

## Step 5: Test It Out

1. **Add a habit**: Enter "Daily Coding" and "Learning", click Add
2. **Complete it**: Click "? Complete Today" button
3. **Check streak**: Should show "?? 1 day streak"
4. **Try again**: Click Complete again - should say "already completed"
5. **Delete it**: Click "??? Delete" and confirm

## ? Troubleshooting

### "npm: command not found"
- Install Node.js from https://nodejs.org/
- Restart your terminal

### "Failed to load habits"
- Make sure API is running on https://localhost:7037
- Open https://localhost:7037/swagger to verify
- Accept any HTTPS certificate warnings

### Port 3000 already in use
- Vite will offer an alternative port (like 3001)
- Or kill the process using port 3000

### Package installation fails
```bash
# Clear npm cache and try again
npm cache clean --force
npm install
```

### Changes not appearing
- Save the file (Ctrl+S)
- Wait 1-2 seconds for auto-reload
- Hard refresh browser: Ctrl+Shift+R

## ?? Quick Customizations

### Change Pink to Blue:
Edit `habit-tracker-ui/src/App.css`

Replace all instances of:
- `#d81b60` ? `#1976d2` (blue)
- `#f8bbd0` ? `#bbdefb` (light blue)
- `#fce4ec` ? `#e3f2fd` (very light blue)

### Change Port:
Edit `habit-tracker-ui/vite.config.js`
```javascript
server: {
  port: 3000,  // Change to any port
```

### Add Your Logo:
1. Put logo.png in `habit-tracker-ui/public/`
2. In `App.jsx`, add:
```javascript
<img src="/logo.png" alt="Logo" />
```

## ?? Project Structure Summary

```
habit-tracker-ui/
??? src/
?   ??? App.jsx          ? MAIN FILE (all logic here)
?   ??? App.css          ? STYLES (pink theme)
?   ??? main.jsx         ? Entry point (don't touch)
?   ??? index.css        ? Global styles
??? public/              ? Static files
??? index.html           ? HTML shell
??? vite.config.js       ? Vite config (proxy setup)
??? package.json         ? Dependencies
??? README.md            ? Documentation
```

**Focus on**: `App.jsx` and `App.css` - these are the only files you need to edit!

## ?? Next Steps

Once it's running:
1. ? Play with the UI - add, complete, delete habits
2. ? Open browser DevTools (F12) and explore
3. ? Make a small change to `App.jsx` and watch it reload
4. ? Change a color in `App.css` and see it update
5. ? Read through `App.jsx` code - it's well commented

## ?? Enjoy!

You now have a complete full-stack habit tracker app with:
- Clean architecture (.NET backend)
- Beautiful UI (React frontend)
- Real-time updates
- Streak calculation
- Full CRUD operations

Happy learning! ??
