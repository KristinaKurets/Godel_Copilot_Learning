import { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { 
  Sparkles, 
  Flame, 
  Calendar, 
  BarChart3, 
  Trophy, 
  Plus, 
  CheckCircle,
  Loader2,
  AlertCircle,
  TrendingUp,
  Target,
  Award,
  Zap,
  Crown,
  Star
} from 'lucide-react';
import './Dashboard.css';

const API_URL = 'http://localhost:5081/api';

function Dashboard() {
  const [statistics, setStatistics] = useState(null);
  const [achievements, setAchievements] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    fetchDashboardData();
  }, []);

  const fetchDashboardData = async () => {
    setLoading(true);
    setError('');
    
    try {
      const [statsResponse, achievementsResponse] = await Promise.all([
        axios.get(`${API_URL}/statistics`),
        axios.get(`${API_URL}/achievements`)
      ]);

      setStatistics(statsResponse.data);
      
      // Get the 3 most recent unlocked achievements
      const unlockedAchievements = achievementsResponse.data
        .filter(a => a.isUnlocked)
        .slice(0, 3);
      setAchievements(unlockedAchievements);
    } catch (err) {
      setError('Failed to load dashboard data. Make sure the API is running.');
      console.error('Error fetching dashboard data:', err);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div className="dashboard">
        <div className="loading-spinner">
          <Loader2 className="spinner" size={60} />
          <p>Loading your dashboard...</p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="dashboard">
        <div className="error-state">
          <AlertCircle className="error-icon" size={80} />
          <h2>Oops! Something went wrong</h2>
          <p>{error}</p>
          <button onClick={fetchDashboardData} className="btn btn-primary">
            Try Again
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="dashboard">
      <header className="dashboard-header">
        <h1>Your Habit Dashboard</h1>
        <p>Track your progress and unlock achievements!</p>
      </header>

      {/* Statistics Grid */}
      <div className="stats-grid">
        <div className="stat-card stat-card-primary">
          <Sparkles className="stat-icon" size={48} />
          <div className="stat-content">
            <h3>Total Habits</h3>
            <p className="stat-value">{statistics?.totalHabits || 0}</p>
            <span className="stat-label">Active</span>
          </div>
        </div>

        <div className="stat-card stat-card-fire">
          <Flame className="stat-icon" size={48} />
          <div className="stat-content">
            <h3>Active Streaks</h3>
            <p className="stat-value">{statistics?.habitsWithActiveStreaks || 0}</p>
            <span className="stat-label">Habits on fire!</span>
          </div>
        </div>

        <div className="stat-card stat-card-success">
          <Calendar className="stat-icon" size={48} />
          <div className="stat-content">
            <h3>This Week</h3>
            <p className="stat-value">{statistics?.completionsThisWeek || 0}</p>
            <span className="stat-label">Completions</span>
          </div>
        </div>

        <div className="stat-card stat-card-info">
          <BarChart3 className="stat-icon" size={48} />
          <div className="stat-content">
            <h3>This Month</h3>
            <p className="stat-value">{statistics?.completionsThisMonth || 0}</p>
            <span className="stat-label">Completions</span>
          </div>
        </div>

        <div className="stat-card stat-card-champion">
          <Trophy className="stat-icon" size={48} />
          <div className="stat-content">
            <h3>Longest Streak</h3>
            <p className="stat-value">{statistics?.longestStreak || 0}</p>
            <span className="stat-label">Days in a row!</span>
          </div>
        </div>
      </div>

      {/* Recent Achievements */}
      {achievements.length > 0 && (
        <div className="achievements-section">
          <div className="section-header">
            <h2>Recent Achievements</h2>
            <Link to="/achievements" className="view-all-link">
              View All &rarr;
            </Link>
          </div>
          <div className="achievements-showcase">
            {achievements.map((achievement) => (
              <div key={achievement.id} className="achievement-badge">
                {getAchievementIcon(achievement.icon)}
                <div className="badge-content">
                  <h4>{achievement.name}</h4>
                  <p>{achievement.description}</p>
                  <span className="badge-unlocked">Unlocked!</span>
                </div>
              </div>
            ))}
          </div>
        </div>
      )}

      {/* Quick Actions */}
      <div className="quick-actions-section">
        <h2>Quick Actions</h2>
        <div className="action-buttons">
          <button
            onClick={() => navigate('/habits')}
            className="action-btn action-btn-primary"
          >
            <Plus className="action-icon" size={36} />
            <div className="action-content">
              <h3>Add New Habit</h3>
              <p>Start building a new routine</p>
            </div>
          </button>

          <button
            onClick={() => navigate('/achievements')}
            className="action-btn action-btn-secondary"
          >
            <Trophy className="action-icon" size={36} />
            <div className="action-content">
              <h3>View Achievements</h3>
              <p>See what you can unlock</p>
            </div>
          </button>

          <button
            onClick={() => navigate('/habits')}
            className="action-btn action-btn-accent"
          >
            <CheckCircle className="action-icon" size={36} />
            <div className="action-content">
              <h3>Complete Today</h3>
              <p>Mark habits as done</p>
            </div>
          </button>
        </div>
      </div>

      {/* Motivational Message */}
      {statistics && (
        <div className="motivation-card">
          <Target className="motivation-icon" size={48} />
          <div className="motivation-content">
            <h3>{getMotivationalMessage(statistics)}</h3>
            <p>{getMotivationalSubtext(statistics)}</p>
          </div>
        </div>
      )}
    </div>
  );
}

// Helper function to get icon component based on icon name
function getAchievementIcon(icon) {
  const iconProps = { className: "badge-icon", size: 48 };
  
  const iconMap = {
    seedling: <TrendingUp {...iconProps} />,
    star: <Star {...iconProps} />,
    crown: <Crown {...iconProps} />,
    fire: <Flame {...iconProps} />,
    gem: <Sparkles {...iconProps} />,
    trophy: <Trophy {...iconProps} />,
    rocket: <Zap {...iconProps} />,
    medal: <Award {...iconProps} />,
    hundred: <Target {...iconProps} />,
    sparkles: <Sparkles {...iconProps} />,
    zap: <Zap {...iconProps} />,
    rainbow: <Award {...iconProps} />
  };
  
  return iconMap[icon] || <Star {...iconProps} />;
}

// Helper function to get motivational message based on stats
function getMotivationalMessage(stats) {
  if (stats.totalHabits === 0) {
    return "Ready to start your journey?";
  }
  if (stats.longestStreak >= 30) {
    return "You're absolutely crushing it!";
  }
  if (stats.longestStreak >= 7) {
    return "You're on fire! Keep the momentum going!";
  }
  if (stats.habitsWithActiveStreaks > 0) {
    return "Great job maintaining your streaks!";
  }
  if (stats.completionsThisWeek > 0) {
    return "Making progress this week!";
  }
  return "Every journey begins with a single step!";
}

function getMotivationalSubtext(stats) {
  if (stats.totalHabits === 0) {
    return "Create your first habit and start building better routines today!";
  }
  if (stats.longestStreak >= 30) {
    return `${stats.longestStreak} days and counting! You're a true champion!`;
  }
  if (stats.longestStreak >= 7) {
    return `${stats.longestStreak} day streak! You're building something amazing!`;
  }
  if (stats.habitsWithActiveStreaks > 0) {
    return `${stats.habitsWithActiveStreaks} active streak${stats.habitsWithActiveStreaks > 1 ? 's' : ''}! Consistency is key!`;
  }
  if (stats.completionsThisWeek > 0) {
    return `${stats.completionsThisWeek} completion${stats.completionsThisWeek > 1 ? 's' : ''} this week. Keep going!`;
  }
  return "Start completing your habits to build momentum!";
}

export default Dashboard;
