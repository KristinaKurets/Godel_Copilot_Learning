import { NavLink, Outlet } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import { Sparkles, LogOut } from 'lucide-react';
import './MainLayout.css';

function MainLayout() {
  const { user, logout } = useAuth();

  return (
    <div className="main-layout">
      <nav className="navbar">
        <div className="navbar-container">
          <NavLink to="/" className="navbar-brand">
            <Sparkles className="brand-icon" size={28} />
            Habit Tracker
          </NavLink>
          <ul className="navbar-menu">
            <li>
              <NavLink to="/" end className="navbar-link">
                Dashboard
              </NavLink>
            </li>
            <li>
              <NavLink to="/habits" className="navbar-link">
                My Habits
              </NavLink>
            </li>
            <li>
              <NavLink to="/achievements" className="navbar-link">
                Achievements
              </NavLink>
            </li>
            <li>
              <NavLink to="/profile" className="navbar-link">
                Profile
              </NavLink>
            </li>
          </ul>
          <div className="navbar-user">
            <span className="user-greeting">Hello, {user}</span>
            <button onClick={logout} className="logout-button">
              <LogOut size={16} />
              Logout
            </button>
          </div>
        </div>
      </nav>
      <main className="main-content">
        <Outlet />
      </main>
    </div>
  );
}

export default MainLayout;
