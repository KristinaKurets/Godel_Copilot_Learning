# Habit Tracker UI

A beautiful React frontend for the Habit Tracker API with a pink-themed interface.

## ?? Quick Start

### Prerequisites
- Node.js 18+ installed
- HabitTracker API running on https://localhost:7037

### Installation

1. Navigate to the UI folder:
```bash
cd habit-tracker-ui
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm run dev
```

4. Open your browser to: **http://localhost:3000**

## ?? Features

- ? View all habits with streak information
- ? Add new habits with name and category
- ? Mark habits complete for today (with duplicate prevention)
- ? Delete habits with confirmation
- ? Beautiful pink gradient theme
- ? Responsive design (mobile-friendly)
- ? Real-time updates after actions
- ? Error handling and user feedback

## ?? Interface

### Main Screen
- **Header**: Welcome message
- **Add Habit Form**: Quick form to create new habits
- **Habits List**: Shows all active habits with:
  - Habit name
  - Category badge
  - Current streak (??)
  - Total completions (??)
  - Complete Today button
  - Delete button

### Color Scheme (Pink Theme)
- Primary: `#ec407a` / `#d81b60`
- Secondary: `#f48fb1` / `#f06292`
- Background: Gradient from `#ffeef8` to `#ffe0f0`
- Accents: `#fce4ec` / `#f8bbd0`

## ?? API Integration

The app connects to your .NET API at `https://localhost:7037`

### Endpoints Used:
- `GET /api/habits` - Fetch all habits
- `POST /api/habits` - Create new habit
- `POST /api/habits/{id}/complete` - Mark complete
- `DELETE /api/habits/{id}` - Delete habit

### Proxy Configuration
The Vite dev server is configured with a proxy to handle HTTPS and CORS:
```javascript
proxy: {
  '/api': {
    target: 'https://localhost:7037',
    changeOrigin: true,
    secure: false
  }
}
```

## ?? Project Structure

```
habit-tracker-ui/
??? src/
?   ??? App.jsx          # Main component with all logic
?   ??? App.css          # Styling
?   ??? main.jsx         # Entry point
?   ??? index.css        # Global styles
??? index.html           # HTML template
??? vite.config.js       # Vite configuration
??? package.json         # Dependencies
??? README.md            # This file
```

## ??? Development

### Available Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build

### Making Changes

1. Edit `src/App.jsx` for functionality
2. Edit `src/App.css` for styling
3. Changes auto-reload in the browser

## ?? Troubleshooting

### API Connection Issues

**Error**: "Failed to load habits"

**Solutions**:
1. Make sure the .NET API is running:
   ```bash
   dotnet run --project ../HabitTracker.API
   ```

2. Verify API is on https://localhost:7037

3. Check browser console for CORS errors

### HTTPS Certificate Warnings

In development, you might see certificate warnings. The app is configured to handle this, but you can:

1. Accept the certificate in your browser
2. Navigate to https://localhost:7037/swagger first
3. Accept the certificate, then return to the React app

### Port Already in Use

If port 3000 is in use:
1. The app will prompt you to use another port
2. Or edit `vite.config.js` to change the port

## ?? Usage Flow

1. **Start the API**:
   ```bash
   dotnet run --project HabitTracker.API
   ```

2. **Start the UI** (in a separate terminal):
   ```bash
   cd habit-tracker-ui
   npm run dev
   ```

3. **Use the app**:
   - Add a new habit using the form
   - Click "Complete Today" to log today's completion
   - Watch your streak grow! ??
   - Delete habits you no longer need

## ?? Responsive Design

The UI is fully responsive and works on:
- ?? Mobile phones (320px+)
- ?? Tablets (768px+)
- ?? Desktops (1024px+)

## ?? Customizing Colors

To change the pink theme, edit `src/App.css`:

```css
/* Primary pink */
--primary: #ec407a;
--primary-dark: #d81b60;

/* Light pink backgrounds */
--bg-light: #fce4ec;
--bg-lighter: #f8bbd0;
```

## ?? Production Build

To build for production:

```bash
npm run build
```

Output will be in the `dist/` folder. Deploy this folder to any static hosting service.

## ?? Tips for Backend Devs

### Understanding React Basics:
- **Components**: Functions that return JSX (HTML-like syntax)
- **State**: Data that changes (use `useState`)
- **Effects**: Side effects like API calls (use `useEffect`)
- **Props**: Data passed between components

### Key Concepts in This App:

```javascript
// State management
const [habits, setHabits] = useState([]);  // Store habits list

// API calls
await axios.get(API_URL);  // Like HttpClient.GetAsync()
await axios.post(API_URL, data);  // Like HttpClient.PostAsJsonAsync()

// Rendering
habits.map(habit => <div>...</div>)  // Like foreach loop
```

### File Watching:
- Vite automatically reloads when you save files
- No need to restart the dev server
- Check browser console for errors

## ?? Learning Resources

- [React Docs](https://react.dev/)
- [Vite Docs](https://vitejs.dev/)
- [Axios Docs](https://axios-http.com/)

## ?? Next Steps

Once comfortable with the basics, you can:
- Add more pages (routing with React Router)
- Add habit editing
- Show completion history calendar
- Add charts for statistics
- Implement user authentication
