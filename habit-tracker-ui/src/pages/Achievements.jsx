import { useState, useEffect } from 'react';
import axios from 'axios';
import { 
  TrendingUp, Flame, Star, Target, Crown, Sparkles, 
  Trophy, Zap, Award, Loader2, AlertCircle 
} from 'lucide-react';
import './Achievements.css';

const API_URL = 'http://localhost:5081/api/achievements';

function Achievements() {
  const [achievements, setAchievements] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    fetchAchievements();
  }, []);

  const fetchAchievements = async () => {
    setLoading(true);
    setError('');
    
    try {
      const response = await axios.get(API_URL);
      setAchievements(response.data);
    } catch (err) {
      setError('Failed to load achievements. Make sure the API is running.');
      console.error('Error fetching achievements:', err);
    } finally {
      setLoading(false);
    }
  };

  const getAchievementIcon = (iconName) => {
    const iconProps = { size: 64 };
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
    return iconMap[iconName] || <Star {...iconProps} />;
  };

  const getProgressPercentage = (current, required) => {
    return Math.min((current / required) * 100, 100);
  };

  if (loading) {
    return (
      <div className="achievements">
        <div className="loading-spinner">
          <Loader2 className="spinner" size={60} />
          <p>Loading achievements...</p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="achievements">
        <div className="error-state">
          <AlertCircle className="error-icon" size={80} />
          <h2>Oops! Something went wrong</h2>
          <p>{error}</p>
          <button onClick={fetchAchievements} className="btn btn-primary">
            Try Again
          </button>
        </div>
      </div>
    );
  }

  const unlockedCount = achievements.filter(a => a.isUnlocked).length;
  const totalCount = achievements.length;

  return (
    <div className="achievements">
      <header className="page-header">
        <h1>Achievements</h1>
        <p>Celebrate your milestones and progress</p>
        <div className="achievements-summary">
          <div className="summary-stat">
            <span className="summary-value">{unlockedCount}</span>
            <span className="summary-label">Unlocked</span>
          </div>
          <div className="summary-divider">/</div>
          <div className="summary-stat">
            <span className="summary-value">{totalCount}</span>
            <span className="summary-label">Total</span>
          </div>
        </div>
      </header>

      <div className="achievements-grid">
        {achievements.map((achievement) => (
          <div 
            key={achievement.id} 
            className={`achievement-card ${achievement.isUnlocked ? 'unlocked' : 'locked'}`}
          >
            <div className="achievement-icon-wrapper">
              {getAchievementIcon(achievement.icon)}
            </div>
            
            <div className="achievement-content">
              <h3>{achievement.name}</h3>
              <p className="achievement-description">{achievement.description}</p>
              
              {achievement.isUnlocked ? (
                <div className="achievement-unlocked">
                  <span className="unlocked-badge">Unlocked!</span>
                </div>
              ) : (
                <div className="achievement-progress">
                  <div className="progress-info">
                    <span className="progress-text">
                      {achievement.currentProgress} / {achievement.requiredProgress}
                    </span>
                    <span className="progress-percentage">
                      {Math.round(getProgressPercentage(achievement.currentProgress, achievement.requiredProgress))}%
                    </span>
                  </div>
                  <div className="progress-bar">
                    <div 
                      className="progress-fill"
                      style={{ 
                        width: `${getProgressPercentage(achievement.currentProgress, achievement.requiredProgress)}%` 
                      }}
                    />
                  </div>
                  <p className="progress-hint">
                    {achievement.requiredProgress - achievement.currentProgress} more to unlock!
                  </p>
                </div>
              )}
            </div>
          </div>
        ))}
      </div>

      {achievements.length === 0 && (
        <div className="empty-state">
          <p>No achievements available yet. Start tracking habits to unlock achievements!</p>
        </div>
      )}
    </div>
  );
}

export default Achievements;
