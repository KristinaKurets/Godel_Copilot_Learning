import './Settings.css';

function Settings() {
  return (
    <div className="settings">
      <header className="page-header">
        <h1>Settings</h1>
        <p>Configure your application preferences</p>
      </header>

      <div className="card">
        <h2>Appearance</h2>
        <div className="settings-section">
          <div className="setting-item">
            <label htmlFor="theme">Theme</label>
            <select id="theme" className="select-input">
              <option>Light</option>
              <option>Dark</option>
              <option>Auto</option>
            </select>
          </div>
          <div className="setting-item">
            <label htmlFor="language">Language</label>
            <select id="language" className="select-input">
              <option>English</option>
              <option>Spanish</option>
              <option>French</option>
            </select>
          </div>
        </div>
      </div>

      <div className="card">
        <h2>Notifications</h2>
        <div className="settings-section">
          <div className="setting-item">
            <label htmlFor="reminder-time">Daily Reminder Time</label>
            <input 
              type="time" 
              id="reminder-time" 
              className="input" 
              defaultValue="09:00"
            />
          </div>
          <div className="setting-item">
            <label htmlFor="reminder-days">Reminder Days</label>
            <div className="checkbox-group">
              <label className="checkbox-label">
                <input type="checkbox" defaultChecked /> Monday
              </label>
              <label className="checkbox-label">
                <input type="checkbox" defaultChecked /> Tuesday
              </label>
              <label className="checkbox-label">
                <input type="checkbox" defaultChecked /> Wednesday
              </label>
              <label className="checkbox-label">
                <input type="checkbox" defaultChecked /> Thursday
              </label>
              <label className="checkbox-label">
                <input type="checkbox" defaultChecked /> Friday
              </label>
              <label className="checkbox-label">
                <input type="checkbox" defaultChecked /> Saturday
              </label>
              <label className="checkbox-label">
                <input type="checkbox" defaultChecked /> Sunday
              </label>
            </div>
          </div>
        </div>
      </div>

      <div className="card">
        <h2>Data Management</h2>
        <div className="settings-section">
          <div className="setting-action">
            <div>
              <h4>Export Data</h4>
              <p>Download all your habit data as JSON</p>
            </div>
            <button className="btn btn-secondary">Export</button>
          </div>
          <div className="setting-action">
            <div>
              <h4>Import Data</h4>
              <p>Import habit data from a JSON file</p>
            </div>
            <button className="btn btn-secondary">Import</button>
          </div>
          <div className="setting-action danger">
            <div>
              <h4>Clear All Data</h4>
              <p>Permanently delete all your habits and progress</p>
            </div>
            <button className="btn btn-danger">Clear Data</button>
          </div>
        </div>
      </div>

      <div className="card">
        <h2>About</h2>
        <div className="settings-section">
          <p><strong>Version:</strong> 1.0.0</p>
          <p><strong>Build:</strong> 2024.01</p>
          <p><strong>Framework:</strong> React 18.3</p>
        </div>
      </div>
    </div>
  );
}

export default Settings;
