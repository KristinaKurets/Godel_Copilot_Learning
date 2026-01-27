# Quick Start Guide for Backend Developers ??

## Setup (5 minutes)

### 1. Install Dependencies
```bash
cd habit-tracker-ui
npm install
```

This is like `dotnet restore` - downloads all packages from `package.json`.

### 2. Start Both Servers

**Terminal 1** - Start the .NET API:
```bash
dotnet run --project HabitTracker.API
```
Should run on: https://localhost:7037

**Terminal 2** - Start the React UI:
```bash
cd habit-tracker-ui
npm run dev
```
Should run on: http://localhost:3000

### 3. Open Browser
Navigate to: **http://localhost:3000**

## ?? What You'll See

A pink-themed interface with:
- Form at the top to add habits
- List of habits below showing streaks
- Buttons to complete or delete each habit

## ?? Understanding the Code (for Backend Devs)

### React Component = C# Class
```javascript
function App() {
  // This is like a C# class
  // Returns JSX (HTML-like markup)
  return <div>...</div>;
}
```

### State = Class Properties
```javascript
const [habits, setHabits] = useState([]);
```
Think of this as:
```csharp
private List<Habit> _habits = new();
```

### useEffect = Constructor/Initialization
```javascript
useEffect(() => {
  fetchHabits();  // Runs when component loads
}, []);
```
Similar to:
```csharp
public App() {
  await FetchHabits();
}
```

### Async/Await - Same as C#!
```javascript
const fetchHabits = async () => {
  const response = await axios.get(API_URL);
  setHabits(response.data);
};
```

Compare to C#:
```csharp
private async Task FetchHabits() {
  var response = await _httpClient.GetAsync(API_URL);
  _habits = await response.Content.ReadFromJsonAsync<List<Habit>>();
}
```

### Axios = HttpClient
```javascript
// GET request
await axios.get(url);

// POST request
await axios.post(url, data);

// DELETE request
await axios.delete(url);
```

Compare to C#:
```csharp
await _httpClient.GetAsync(url);
await _httpClient.PostAsJsonAsync(url, data);
await _httpClient.DeleteAsync(url);
```

## ?? Common Tasks

### Change API URL
Edit `src/App.jsx`:
```javascript
const API_URL = 'https://localhost:7037/api/habits';
```

### Change Colors
Edit `src/App.css` - search for color codes like `#d81b60` and replace.

### Add New Field to Form
In `src/App.jsx`:

1. Add to state:
```javascript
const [newHabit, setNewHabit] = useState({ 
  name: '', 
  category: '',
  description: ''  // New field
});
```

2. Add input:
```javascript
<input
  type="text"
  placeholder="Description"
  value={newHabit.description}
  onChange={(e) => setNewHabit({ ...newHabit, description: e.target.value })}
  className="input"
/>
```

3. Update POST request (already sends all fields)

### Debug in Browser
1. Open Developer Tools: `F12`
2. Check **Console** tab for errors
3. Check **Network** tab for API calls
4. Add `console.log('value:', value)` anywhere in code

## ?? Common Issues

### "Failed to load habits"
- ? Check if API is running
- ? Check API URL is correct
- ? Accept HTTPS certificate at https://localhost:7037/swagger

### "npm: command not found"
- ? Install Node.js from https://nodejs.org/
- ? Restart your terminal

### Changes not showing
- ? Save the file (`Ctrl+S`)
- ? Wait 1-2 seconds for auto-reload
- ? Hard refresh browser: `Ctrl+Shift+R`

### CORS Errors
The Vite proxy should handle this, but if you see CORS errors:
1. Make sure you're accessing `http://localhost:3000` (not the API URL)
2. Check `vite.config.js` proxy settings
3. Ensure API allows CORS (it should by default)

## ?? Key Differences from C#

| Concept | C# | JavaScript/React |
|---------|----|--------------------|
| Variables | `var name = "test";` | `const name = "test";` |
| Arrays | `List<Habit>` | `const habits = []` |
| Objects | `new Habit { Name = "Exercise" }` | `{ name: "Exercise" }` |
| Null check | `if (habit != null)` | `if (habit)` |
| String template | `$"Hello {name}"` | `` `Hello ${name}` `` |
| Arrow function | `() => DoSomething()` | `() => doSomething()` |
| Properties | `habit.Name` | `habit.name` |

## ?? React Concepts

### JSX - HTML in JavaScript
```javascript
return (
  <div className="card">
    <h1>Hello</h1>
  </div>
);
```

### Props - Passing Data
```javascript
// Parent component
<HabitItem habit={habit} onComplete={handleComplete} />

// Child component
function HabitItem({ habit, onComplete }) {
  return <div>{habit.name}</div>;
}
```

### State Updates
```javascript
// ? Wrong (mutating state)
habits.push(newHabit);

// ? Correct (creating new array)
setHabits([...habits, newHabit]);
```

### Conditional Rendering
```javascript
{loading ? <p>Loading...</p> : <div>Content</div>}
{habits.length === 0 && <p>No habits</p>}
```

## ??? Project Structure

```
habit-tracker-ui/
??? src/
?   ??? App.jsx          ? Main component (like Program.cs + Controller)
?   ??? App.css          ? Styles
?   ??? main.jsx         ? Entry point (like Program.cs Main())
?   ??? index.css        ? Global styles
??? index.html           ? HTML shell
??? vite.config.js       ? Config (like appsettings.json)
??? package.json         ? Dependencies (like .csproj)
```

## ?? Typical Development Flow

1. Make changes in `src/App.jsx` or `src/App.css`
2. Save file (`Ctrl+S`)
3. Browser auto-reloads (no restart needed!)
4. Check browser for changes
5. Check console for errors

## ?? Quick Wins

### Add a Loading Spinner
Replace "Loading habits..." text with an animated spinner.

### Add Input Validation
```javascript
if (!newHabit.name.trim()) {
  setError('Name is required');
  return;
}
```

### Add Toast Notifications
Install a toast library:
```bash
npm install react-hot-toast
```

### Show Today's Date
```javascript
<p>Today: {new Date().toLocaleDateString()}</p>
```

## ?? Next Steps

Once you're comfortable:
1. Split `App.jsx` into smaller components
2. Add routing with React Router
3. Add form validation library (Formik/React Hook Form)
4. Add a UI library (Material-UI, Chakra UI)
5. Add state management (if app grows - Zustand, Redux)

## ?? Dependencies Explained

- **react** - Core React library (like .NET framework)
- **react-dom** - Renders React to browser (like Kestrel)
- **axios** - HTTP client (like HttpClient)
- **vite** - Build tool and dev server (like dotnet CLI)
- **@vitejs/plugin-react** - React support for Vite

## ?? You're All Set!

The UI is simple and focused on functionality. As a backend dev, you'll find:
- ? Clear separation of concerns
- ? Async/await patterns you know
- ? Familiar error handling
- ? Type safety (with JSDoc or TypeScript later)

Happy coding! ??
