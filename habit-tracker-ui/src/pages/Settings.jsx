import { useState } from 'react';
import { useAuth } from '../contexts/AuthContext';
import { User, Download, Trash2, Save, AlertTriangle } from 'lucide-react';
import './Settings.css';

function Settings() {
  const { user } = useAuth();
  const [username, setUsername] = useState(user || '');
  const [showDeleteWarning, setShowDeleteWarning] = useState(false);

  const handleSaveUsername = () => {
    alert(`Username would be saved as: ${username}`);
  };

  const handleExportData = () => {
    alert('Data export functionality would be triggered here');
  };

  const handleDeleteAllHabits = () => {
    if (showDeleteWarning) {
      alert('All habits would be deleted (this is just a demo)');
      setShowDeleteWarning(false);
    } else {
      setShowDeleteWarning(true);
    }
  };

  return (
    <div className="settings">
      <header className="settings-header">
        <h1>Settings</h1>
        <p>Manage your account and application preferences</p>
      </header>

      {/* User Settings Card */}
      <div className="settings-card">
        <div className="card-header">
          <User size={24} />
          <h2>Account Settings</h2>
        </div>
        <div className="card-content">
          <div className="setting-group">
            <label htmlFor="username">Username</label>
            <div className="input-with-button">
              <input
                type="text"
                id="username"
                className="settings-input"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                placeholder="Enter your username"
              />
              <button 
                className="btn btn-primary"
                onClick={handleSaveUsername}
              >
                <Save size={18} />
                Save
              </button>
            </div>
            <p className="setting-hint">This is a demo - username changes won't persist</p>
          </div>
        </div>
      </div>

      {/* Data Management Card */}
      <div className="settings-card">
        <div className="card-header">
          <Download size={24} />
          <h2>Data Management</h2>
        </div>
        <div className="card-content">
          <div className="setting-action">
            <div className="action-info">
              <h3>Export Your Data</h3>
              <p>Download all your habits, completions, and progress as a JSON file</p>
            </div>
            <button 
              className="btn btn-secondary"
              onClick={handleExportData}
            >
              <Download size={18} />
              Export Data
            </button>
          </div>
        </div>
      </div>

      {/* Danger Zone Card */}
      <div className="settings-card danger-card">
        <div className="card-header">
          <AlertTriangle size={24} />
          <h2>Danger Zone</h2>
        </div>
        <div className="card-content">
          <div className="setting-action">
            <div className="action-info">
              <h3>Delete All Habits</h3>
              <p>Permanently delete all your habits and progress. This action cannot be undone.</p>
            </div>
            <button 
              className="btn btn-danger"
              onClick={handleDeleteAllHabits}
            >
              <Trash2 size={18} />
              {showDeleteWarning ? 'Click Again to Confirm' : 'Delete All Habits'}
            </button>
          </div>
          
          {showDeleteWarning && (
            <div className="warning-message">
              <AlertTriangle size={20} />
              <span>Are you sure? Click the button again to confirm deletion.</span>
              <button 
                className="btn-link"
                onClick={() => setShowDeleteWarning(false)}
              >
                Cancel
              </button>
            </div>
          )}
        </div>
      </div>

      {/* Info Section */}
      <div className="settings-info">
        <h3>About This Page</h3>
        <p>
          This Settings page demonstrates the <strong>AdminLayout</strong> with a sidebar navigation.
          Notice how the layout differs from the main app pages - it uses a different navigation structure
          optimized for administrative tasks.
        </p>
        <p className="info-note">
          Note: All actions on this page are mock implementations and won't actually modify data.
        </p>
      </div>
    </div>
  );
}

export default Settings;
