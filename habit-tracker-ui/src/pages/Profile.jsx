import { useAuth } from '../contexts/AuthContext';
import { User } from 'lucide-react';
import './Profile.css';

function Profile() {
  const { user } = useAuth();

  return (
    <div className="profile">
      <header className="page-header">
        <h1>Profile</h1>
        <p>Manage your account and preferences</p>
      </header>

      <div className="profile-grid">
        <div className="card profile-card">
          <div className="profile-avatar">
            <User className="avatar-icon" size={64} />
          </div>
          <h2>{user}</h2>
          <p className="profile-email">{user}@habittracker.com</p>
          <button className="btn btn-secondary">Edit Profile</button>
        </div>

        <div className="card stats-card">
          <h3>Your Stats</h3>
          <div className="stats-list">
            <div className="stat-item">
              <span className="stat-label">Member Since</span>
              <span className="stat-value">January 2024</span>
            </div>
            <div className="stat-item">
              <span className="stat-label">Total Habits</span>
              <span className="stat-value">0</span>
            </div>
            <div className="stat-item">
              <span className="stat-label">Longest Streak</span>
              <span className="stat-value">0 days</span>
            </div>
            <div className="stat-item">
              <span className="stat-label">Total Completions</span>
              <span className="stat-value">0</span>
            </div>
          </div>
        </div>
      </div>

      <div className="card">
        <h2>Preferences</h2>
        <div className="preferences-list">
          <div className="preference-item">
            <div>
              <h4>Email Notifications</h4>
              <p>Receive daily reminders for your habits</p>
            </div>
            <label className="toggle">
              <input type="checkbox" />
              <span className="toggle-slider"></span>
            </label>
          </div>
          <div className="preference-item">
            <div>
              <h4>Weekly Report</h4>
              <p>Get a summary of your weekly progress</p>
            </div>
            <label className="toggle">
              <input type="checkbox" />
              <span className="toggle-slider"></span>
            </label>
          </div>
          <div className="preference-item">
            <div>
              <h4>Achievement Alerts</h4>
              <p>Get notified when you unlock achievements</p>
            </div>
            <label className="toggle">
              <input type="checkbox" defaultChecked />
              <span className="toggle-slider"></span>
            </label>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Profile;
