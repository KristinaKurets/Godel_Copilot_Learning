import { useState } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { Sparkles } from 'lucide-react';
import './LoginPage.css';

function LoginPage() {
  const [username, setUsername] = useState('');
  const [error, setError] = useState('');
  const { login } = useAuth();

  const handleSubmit = (e) => {
    e.preventDefault();
    
    if (!username.trim()) {
      setError('Please enter a username');
      return;
    }

    if (username.trim().length < 3) {
      setError('Username must be at least 3 characters');
      return;
    }

    login(username.trim());
  };

  return (
    <div className="login-page">
      <div className="login-container">
        <div className="login-header">
          <Sparkles className="login-icon" size={64} />
          <h1>Habit Tracker</h1>
          <p>Build better habits, one day at a time</p>
        </div>

        <div className="login-card">
          <h2>Welcome!</h2>
          <p className="login-subtitle">Enter your username to get started</p>

          <form onSubmit={handleSubmit} className="login-form">
            <div className="form-group">
              <label htmlFor="username">Username</label>
              <input
                type="text"
                id="username"
                className="login-input"
                placeholder="Enter your username"
                value={username}
                onChange={(e) => {
                  setUsername(e.target.value);
                  setError('');
                }}
                autoFocus
              />
            </div>

            {error && <div className="login-error">{error}</div>}

            <button type="submit" className="login-button">
              Login
            </button>
          </form>

          <div className="login-info">
            <p>Note: This is a demo app - just enter any username to continue</p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default LoginPage;
