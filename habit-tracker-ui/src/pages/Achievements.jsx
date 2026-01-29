import { TrendingUp, Flame, Star, Target, Crown, Sparkles } from 'lucide-react';
import './Achievements.css';

function Achievements() {
  return (
    <div className="achievements">
      <header className="page-header">
        <h1>Achievements</h1>
        <p>Celebrate your milestones and progress</p>
      </header>

      <div className="achievements-grid">
        <div className="achievement-card locked">
          <TrendingUp className="achievement-icon" size={64} />
          <h3>First Step</h3>
          <p>Complete your first habit</p>
          <span className="achievement-status">Locked</span>
        </div>

        <div className="achievement-card locked">
          <Flame className="achievement-icon" size={64} />
          <h3>Week Warrior</h3>
          <p>Maintain a 7-day streak</p>
          <span className="achievement-status">Locked</span>
        </div>

        <div className="achievement-card locked">
          <Star className="achievement-icon" size={64} />
          <h3>Habit Master</h3>
          <p>Create 10 habits</p>
          <span className="achievement-status">Locked</span>
        </div>

        <div className="achievement-card locked">
          <Target className="achievement-icon" size={64} />
          <h3>Consistency King</h3>
          <p>Complete 100 habits</p>
          <span className="achievement-status">Locked</span>
        </div>

        <div className="achievement-card locked">
          <Crown className="achievement-icon" size={64} />
          <h3>Legend</h3>
          <p>Maintain a 30-day streak</p>
          <span className="achievement-status">Locked</span>
        </div>

        <div className="achievement-card locked">
          <Sparkles className="achievement-icon" size={64} />
          <h3>Perfectionist</h3>
          <p>100% completion rate for a week</p>
          <span className="achievement-status">Locked</span>
        </div>
      </div>

      <div className="card">
        <h2>Coming Soon</h2>
        <p>More achievements will be unlocked as you continue your habit tracking journey. Keep going!</p>
      </div>
    </div>
  );
}

export default Achievements;
