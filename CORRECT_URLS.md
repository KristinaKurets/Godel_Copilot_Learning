# ? UPDATED - Correct URLs

## ?? Application URLs

| Component | URL | Notes |
|-----------|-----|-------|
| **React UI** | http://localhost:3000 | Frontend (Vite dev server) |
| **API** | http://localhost:5081 | Backend (.NET API) |
| **Swagger** | http://localhost:5081/swagger | API documentation |

---

## ?? How to Start

### Option 1: Use Batch Script
Double-click: **`start-app.bat`**

### Option 2: Manual Start

**Terminal 1 - API:**
```bash
dotnet run --project HabitTracker.API
```
Should show: `Now listening on: http://localhost:5081`

**Terminal 2 - UI:**
```bash
cd habit-tracker-ui
npm run dev
```
Should show: `Local: http://localhost:3000/`

---

## ? Verification

### 1. Check API is Running
Open: **http://localhost:5081/swagger**

Should show Swagger UI with 4 endpoints.

### 2. Check UI is Running
Open: **http://localhost:3000**

Should show pink themed Habit Tracker interface.

### 3. Test Full Stack
- Go to http://localhost:3000
- You should see 4 sample habits
- Try clicking "? Complete Today"
- Should work and update the streak

---

## ?? Important Notes:

**You need TWO terminal windows**:
1. **Terminal 1**: Running `npm run dev` (React) - **Keep it open!**
2. **Terminal 2**: Running `dotnet run --project HabitTracker.API` (API)

**Or use**: Double-click `start-app.bat` which opens both automatically!

---

## ?? Summary

**Updated Configuration:**
- ? API URL: **http://localhost:5081** (HTTP, not HTTPS)
- ? UI URL: **http://localhost:3000**
- ? Vite proxy configured
- ? CORS enabled for React app
- ? No HTTPS certificate issues!

Just restart both servers and you're good to go! ????
