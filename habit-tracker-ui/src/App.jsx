import { useState, useEffect } from 'react';
import axios from 'axios';
import './App.css';

const API_URL = 'http://localhost:5081/api/habits';

function App() {
  const [habits, setHabits] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');
  const [newHabit, setNewHabit] = useState({ name: '', category: '' });

  const fetchHabits = async () => {
    setLoading(true);
    setError('');
    try {
      const response = await axios.get(API_URL);
      setHabits(response.data);
    } catch (err) {
      setError('Failed to load habits. Make sure the API is running on http://localhost:5081');
      console.error('Error fetching habits:', err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchHabits();
  }, []);

  const handleCreateHabit = async (e) => {
    e.preventDefault();
    if (!newHabit.name.trim() || !newHabit.category.trim()) {
      setError('Please enter both name and category');
      return;
    }

    setLoading(true);
    setError('');
    setMessage('');

    try {
      await axios.post(API_URL, {
        name: newHabit.name,
        category: newHabit.category
      });
      setNewHabit({ name: '', category: '' });
      setMessage('Habit created successfully!');
      await fetchHabits();
      setTimeout(() => setMessage(''), 3000);
    } catch (err) {
      setError('Failed to create habit');
      console.error('Error creating habit:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleCompleteHabit = async (habitId) => {
    setLoading(true);
    setError('');
    setMessage('');

    try {
      await axios.post(`${API_URL}/${habitId}/complete`);
      setMessage('Habit marked complete for today!');
      await fetchHabits();
      setTimeout(() => setMessage(''), 3000);
    } catch (err) {
      setMessage('You already completed this habit today!');
      console.error('Error completing habit:', err);
      setTimeout(() => setMessage(''), 3000);
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteHabit = async (habitId) => {
    if (!window.confirm('Are you sure you want to delete this habit?')) {
      return;
    }

    setLoading(true);
    setError('');
    setMessage('');

    try {
      await axios.delete(`${API_URL}/${habitId}`);
      setMessage('Habit deleted successfully!');
      await fetchHabits();
      setTimeout(() => setMessage(''), 3000);
    } catch (err) {
      setError('Failed to delete habit');
      console.error('Error deleting habit:', err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="app">
      <header className="header">
        <h1>Habit Tracker</h1>
        <p>Build better habits, one day at a time</p>
      </header>

      <div className="card">
        <h2>Add New Habit</h2>
        <form onSubmit={handleCreateHabit} className="habit-form">
          <div className="form-row">
            <input
              type="text"
              placeholder="Habit name (e.g., Exercise)"
              value={newHabit.name}
              onChange={(e) => setNewHabit({ ...newHabit, name: e.target.value })}
              className="input"
              disabled={loading}
            />
            <input
              type="text"
              placeholder="Category (e.g., Fitness)"
              value={newHabit.category}
              onChange={(e) => setNewHabit({ ...newHabit, category: e.target.value })}
              className="input"
              disabled={loading}
            />
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Adding...' : '+ Add Habit'}
            </button>
          </div>
        </form>
      </div>

      {error && <div className="alert alert-error">{error}</div>}
      {message && <div className="alert alert-success">{message}</div>}

      <div className="card">
        <h2>My Habits</h2>
        {loading && habits.length === 0 ? (
          <p className="loading">Loading habits...</p>
        ) : habits.length === 0 ? (
          <p className="empty-state">No habits yet. Create your first habit above!</p>
        ) : (
          <div className="habits-list">
            {habits.map((habit) => (
              <div key={habit.id} className="habit-item">
                <div className="habit-info">
                  <h3>{habit.name}</h3>
                  <span className="category-badge">{habit.category}</span>
                  <div className="habit-stats">
                    <span className="streak">Streak: {habit.currentStreak} days</span>
                    <span className="total">Total: {habit.totalCompletions} completions</span>
                  </div>
                </div>
                <div className="habit-actions">
                  <button
                    onClick={() => handleCompleteHabit(habit.id)}
                    className="btn btn-complete"
                    disabled={loading}
                  >
                    Complete Today
                  </button>
                  <button
                    onClick={() => handleDeleteHabit(habit.id)}
                    className="btn btn-delete"
                    disabled={loading}
                  >
                    Delete
                  </button>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>

      <footer className="footer">
        <p>Keep going! Consistency is key!</p>
      </footer>
    </div>
  );
}

export default App;
