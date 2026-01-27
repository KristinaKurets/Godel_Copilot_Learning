# HabitTracker - Complete Application Setup

## ??? Project Structure

```
C:\Users\k.kurets\source\repos\
??? HabitTracker.API/              # .NET Web API
??? HabitTracker.Application/      # Business logic
??? HabitTracker.Domain/           # Entities
??? HabitTracker.Infrastructure/   # Data access
??? HabitTracker.Tests/            # Unit tests
??? habit-tracker-ui/              # React frontend ? NEW
    ??? src/
    ?   ??? App.jsx                # Main React component
    ?   ??? App.css                # Pink theme styles
    ?   ??? main.jsx               # Entry point
    ?   ??? index.css              # Global styles
    ??? index.html
    ??? vite.config.js             # Vite + proxy config
    ??? package.json
    ??? README.md
```

## ?? Getting Started

### Option 1: Start Everything at Once (Easiest)

Double-click: **`start-app.bat`**

This opens two terminal windows:
1. API running on https://localhost:7037
2. UI running on http://localhost:3000

### Option 2: Start Manually

**Step 1** - Install UI dependencies (first time only):
```bash
cd habit-tracker-ui
npm install
```

**Step 2** - Start API (Terminal 1):
```bash
dotnet run --project HabitTracker.API
```

**Step 3** - Start UI (Terminal 2):
```bash
cd habit-tracker-ui
npm run dev
```

**Step 4** - Open browser:
- UI: http://localhost:3000
- API Swagger: https://localhost:7037/swagger

## ?? React Frontend Features

### What's Included:
? **Add New Habits** - Form with name and category inputs
? **View All Habits** - List with streak and completion stats
? **Complete Today Button** - Mark habit complete for today
? **Delete Button** - Remove habits with confirmation
? **Error Messages** - Clear feedback for API errors
? **Success Messages** - Confirmation when actions succeed
? **Loading States** - Shows when API calls are in progress
? **Duplicate Prevention** - Shows message if already completed today
? **Responsive Design** - Works on mobile and desktop
? **Pink Theme** - Beautiful gradient pink interface ??

### API Integration:
- Axios configured to call `https://localhost:7037`
- Vite proxy handles CORS and HTTPS
- Auto-refresh after create/delete/complete actions

## ?? Testing the Full Stack

### 1. Create a Habit
- Fill in "Habit name" (e.g., "Morning Run")
- Fill in "Category" (e.g., "Fitness")
- Click "Add Habit"
- See it appear in the list below

### 2. Mark Complete
- Click "? Complete Today" button
- Streak should increase by 1
- List refreshes automatically

### 3. Complete Again (Same Day)
- Click "? Complete Today" again
- Should show: "?? You already completed this habit today!"
- Prevents duplicate entries

### 4. Delete Habit
- Click "??? Delete" button
- Confirm the popup
- Habit is removed from list

### 5. Check Streak Calculation
- Complete a habit for multiple days in a row
- Streak counter should increase
- Skip a day, then complete again
- Streak should reset to 1

## ??? File Overview

### Backend Files (Already Done)
- ? Domain entities with relationships
- ? Application DTOs and interfaces
- ? Streak calculator implementation
- ? Repository with use case methods
- ? SQLite database with seed data
- ? 24 passing unit tests

### Frontend Files (New)
| File | Purpose | C# Equivalent |
|------|---------|---------------|
| `package.json` | Dependencies | `.csproj` file |
| `vite.config.js` | Build config | `appsettings.json` |
| `src/main.jsx` | Entry point | `Program.cs` Main() |
| `src/App.jsx` | Main component | Controller + Service |
| `src/App.css` | Styles | CSS (no direct equivalent) |
| `index.html` | HTML shell | Razor view |

## ?? Key React Concepts (for Backend Devs)

### 1. Component State
```javascript
const [habits, setHabits] = useState([]);
```
- `habits` - Current value (readonly)
- `setHabits` - Update function (creates new state)

### 2. Effects (Side Effects)
```javascript
useEffect(() => {
  fetchHabits();  // Runs on component mount
}, []);  // Empty array = run once
```

### 3. Event Handlers
```javascript
<button onClick={() => handleComplete(habit.id)}>
  Complete
</button>
```

### 4. Conditional Rendering
```javascript
{loading ? <p>Loading...</p> : <div>Content</div>}
{error && <div className="alert">{error}</div>}
```

### 5. Mapping Arrays (Like foreach)
```javascript
{habits.map(habit => (
  <div key={habit.id}>{habit.name}</div>
))}
```

## ??? Development Tips

### Making Changes:

**To change API URL:**
Edit `src/App.jsx`, line 6:
```javascript
const API_URL = 'https://localhost:7037/api/habits';
```

**To change colors:**
Edit `src/App.css` - search/replace color codes:
- `#d81b60` - Main pink
- `#f8bbd0` - Light pink
- `#fce4ec` - Very light pink

**To add a new button:**
```javascript
<button onClick={handleSomething} className="btn btn-primary">
  Click Me
</button>
```

### Browser DevTools (F12):

**Console Tab:**
- See `console.log()` output
- See errors (red text)
- Like Debug Output window in VS

**Network Tab:**
- See API requests
- Check response data
- Like Fiddler/Postman

**Elements Tab:**
- Inspect HTML/CSS
- Modify styles live
- See what's rendered

## ?? Workflow

### Typical Development Session:

1. **Start servers** (both API and UI)
2. **Open browser** to http://localhost:3000
3. **Open F12 DevTools** (Console + Network tabs)
4. **Make changes** to `App.jsx` or `App.css`
5. **Save file** - browser auto-reloads!
6. **Check result** in browser
7. **Check console** for errors

### No need to:
- ? Restart servers (auto-reload)
- ? Rebuild (Vite does it automatically)
- ? Clear cache (usually)

## ?? Package Management

### Installing Packages (Like NuGet)
```bash
npm install package-name
```

Example:
```bash
npm install react-icons
```

Then use:
```javascript
import { FaFire } from 'react-icons/fa';
```

### Updating Packages
```bash
npm update
```

## ?? Customization Ideas

### Easy Changes:
1. Change emoji icons (?? ? ?)
2. Modify button text
3. Adjust colors
4. Change font sizes

### Medium Changes:
1. Add habit description field
2. Show creation date
3. Add filter by category
4. Sort habits by streak

### Advanced Changes:
1. Split into multiple components
2. Add routing (separate pages)
3. Add charts/graphs
4. Add animations

## ? Verification Checklist

After setup, verify:
- [ ] API runs on https://localhost:7037
- [ ] UI runs on http://localhost:3000
- [ ] Can see seed data in UI (4 sample habits)
- [ ] Can create new habit
- [ ] Can mark habit complete
- [ ] Can delete habit
- [ ] Streak counter works
- [ ] No console errors

## ?? Getting Help

### Console Errors:
1. Read the error message (usually tells you the problem)
2. Check which line number
3. Google the error if unclear

### API Not Responding:
1. Check API terminal - is it running?
2. Visit https://localhost:7037/swagger
3. Test endpoints in Swagger first
4. Check Network tab in browser DevTools

### UI Not Loading:
1. Check terminal for errors
2. Try `npm install` again
3. Delete `node_modules` folder and reinstall
4. Check if port 3000 is available

## ?? Learning Resources

### React Basics:
- [React.dev](https://react.dev/learn) - Official tutorial
- [JavaScript.info](https://javascript.info/) - Modern JS

### Video Tutorials:
- Search YouTube: "React for beginners"
- Search YouTube: "React crash course"

### For Backend Devs:
- "React for C# Developers"
- "Frontend for Backend Developers"

## ?? You Did It!

You now have:
- ? Full-stack application (React + .NET)
- ? Working API with SQLite database
- ? Beautiful pink UI
- ? Complete CRUD operations
- ? Streak tracking functionality
- ? 24 passing unit tests

**Next**: Play with it, break it, fix it. That's how you learn! ??

### Quick Commands Reference:

```bash
# Install dependencies (first time)
cd habit-tracker-ui
npm install

# Start UI
npm run dev

# Start API (from repo root)
dotnet run --project HabitTracker.API

# Run tests
dotnet test HabitTracker.Tests\HabitTracker.Tests.csproj
```

Enjoy building! ??
