import { useState, useEffect } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { 
  User, 
  Calendar, 
  Trophy, 
  Target, 
  Award, 
  TrendingUp,
  Loader2,
  AlertCircle 
} from 'lucide-react';
import './Profile.css';

const API_URL = 'http://localhost:5081/api/profile';

function Profile() {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [profile, setProfile] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    fetchProfile();
  }, []);

  const fetchProfile = async () => {
    setLoading(true);
    setError('');
    
    try {
      const response = await axios.get(API_URL);
      setProfile(response.data);
    } catch (err) {
      setError('Failed to load profile data. Make sure the API is running.');
      console.error('Error fetching profile:', err);
    } finally {
      setLoading(false);
    }
  };

  const formatDate = (dateString) => {
    if (!dateString) return 'Recently';
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', { month: 'long', year: 'numeric' });
  };

  const getProgressToNextLevel = () => {
    if (!profile) return 0;
    const currentLevelCompletions = profile.level * 10;
    const completionsInCurrentLevel = profile.totalCompletions - currentLevelCompletions;
    return (completionsInCurrentLevel / 10) * 100;
  };

  const getCompletionsToNextLevel = () => {
    if (!profile) return 10;
    const currentLevelCompletions = profile.level * 10;
    const nextLevelCompletions = (profile.level + 1) * 10;
    return nextLevelCompletions - profile.totalCompletions;
  };

  if (loading) {
    return (
      <div className="profile">
        <div className="loading-spinner">
          <Loader2 className="spinner" size={60} />
          <p>Loading profile...</p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="profile">
        <div className="error-state">
          <AlertCircle className="error-icon" size={80} />
          <h2>Oops! Something went wrong</h2>
          <p>{error}</p>
          <button onClick={fetchProfile} className="btn btn-primary">
            Try Again
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="profile">
      <header className="page-header">
        <h1>Player Profile</h1>
        <p>Your habit tracking journey</p>
      </header>

      {/* Main Profile Card */}
      <div className="profile-card-main">
        <div className="profile-header-section">
          <div className="profile-avatar-large">
            <User className="avatar-icon-large" size={80} />
            <div className="level-badge">
              <span className="level-number">{profile?.level || 0}</span>
            </div>
          </div>
          
          <div className="profile-info">
            <h2 className="profile-username">{user || profile?.username}</h2>
            <div className="profile-meta">
              <Calendar size={16} />
              <span>Member since {formatDate(profile?.memberSince)}</span>
            </div>
          </div>
        </div>

        {/* Level Progress */}
        <div className="level-section">
          <div className="level-header">
            <h3>Level {profile?.level || 0}</h3>
            <span className="next-level">Next: Level {(profile?.level || 0) + 1}</span>
          </div>
          <div className="level-progress-bar">
            <div 
              className="level-progress-fill"
              style={{ width: `${getProgressToNextLevel()}%` }}
            >
              <span className="progress-glow"></span>
            </div>
          </div>
          <p className="level-hint">
            <TrendingUp size={14} />
            {getCompletionsToNextLevel()} more completions to level up!
          </p>
        </div>

        {/* Stats Grid */}
        <div className="profile-stats-grid">
          <div className="profile-stat-card">
            <div className="stat-icon-wrapper stat-primary">
              <Target size={32} />
            </div>
            <div className="stat-content">
              <span className="stat-value">{profile?.totalCompletions || 0}</span>
              <span className="stat-label">Total Completions</span>
            </div>
          </div>

          <div className="profile-stat-card">
            <div className="stat-icon-wrapper stat-success">
              <Trophy size={32} />
            </div>
            <div className="stat-content">
              <span className="stat-value">{profile?.achievementsUnlocked || 0}</span>
              <span className="stat-label">Achievements</span>
            </div>
          </div>

          <div className="profile-stat-card">
            <div className="stat-icon-wrapper stat-info">
              <Award size={32} />
            </div>
            <div className="stat-content">
              <span className="stat-value">Level {profile?.level || 0}</span>
              <span className="stat-label">Current Level</span>
            </div>
          </div>
        </div>

        {/* Quick Actions */}
        <div className="profile-actions">
          <button 
            className="btn btn-primary"
            onClick={() => navigate('/achievements')}
          >
            <Trophy size={18} />
            View Achievements
          </button>
          <button 
            className="btn btn-secondary"
            onClick={() => navigate('/habits')}
          >
            <Target size={18} />
            View Habits
          </button>
        </div>
      </div>

      {/* Preferences Section */}
      <div className="card preferences-card">
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
