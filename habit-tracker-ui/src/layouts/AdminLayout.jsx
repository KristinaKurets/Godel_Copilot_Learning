import { NavLink, Outlet } from 'react-router-dom';
import { Settings, ArrowLeft } from 'lucide-react';
import './AdminLayout.css';

function AdminLayout() {
  return (
    <div className="admin-layout">
      <aside className="sidebar">
        <div className="sidebar-header">
          <h2>Admin Panel</h2>
        </div>
        <nav className="sidebar-nav">
          <NavLink to="/admin/settings" className="sidebar-link">
            <Settings className="sidebar-icon" size={20} />
            <span>Settings</span>
          </NavLink>
          <NavLink to="/" className="sidebar-link sidebar-link-back">
            <ArrowLeft className="sidebar-icon" size={20} />
            <span>Back to Main</span>
          </NavLink>
        </nav>
      </aside>
      <main className="admin-content">
        <Outlet />
      </main>
    </div>
  );
}

export default AdminLayout;
