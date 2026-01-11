import React, { useEffect, useState } from 'react';
import '../css/PopupMenu.css';

const PopupMenu = ({ isOpen, onClose, navigateTo, currentPage }) => {
  const [shouldRender, setShouldRender] = useState(false);

  useEffect(() => {
    if (isOpen) {
      setShouldRender(true);
    } else {
      const timer = setTimeout(() => setShouldRender(false), 300);
      return () => clearTimeout(timer);
    }
  }, [isOpen]);

  if (!shouldRender) return null;

  const handleItemClick = (page) => {
    if (navigateTo) navigateTo(page); 
    onClose();
  };

  // placeholder admin actions (does not change page, stays in Admin Mode)
  const handleAdminPlaceholder = (action) => {
    console.log(`Admin Action triggered: ${action}`); 
    onClose();
  };

  const isAdmin = currentPage === 'admin';

  return (
    <div 
      className={`popup-overlay ${isOpen ? 'open' : 'closing'}`} 
      onClick={onClose}
    >
      <div 
        className={`popup-content ${isOpen ? 'open' : 'closing'}`}
        onClick={(e) => e.stopPropagation()} 
      >
        <ul className="popup-list">
          {isAdmin ? (
            <>
              {/* Admin Actions */}
              <li onClick={() => handleAdminPlaceholder('profile')}>Profile</li>
              <li onClick={() => handleAdminPlaceholder('manage_history')}>Manage History</li>
              <li onClick={() => handleAdminPlaceholder('transaction_history')}>Transaction History</li>
              
              {/* UPDATED: Now triggers the 'create_admin' event */}
              <li onClick={() => handleItemClick('create_admin')}>Create Admin Account</li>
              
              {/* Nav Actions */}
              <li className="logout-item" onClick={() => handleItemClick('home')}>Log Out</li>
              <li onClick={() => handleItemClick('home')}>Demo - User Mode</li>
            </>
          ) : (
            
          /* User Mode Menu */
            <>
              <li onClick={() => handleItemClick('profile')}>Profile</li>
              
              {/* Mobile Only Links */}
              <li className="mobile-only" onClick={() => handleItemClick('menu')}>Menu</li>
              <li className="mobile-only" onClick={() => handleItemClick('orders')}>Orders</li>
              
              <li onClick={() => handleItemClick('wallet')}>Wallet</li>
              <li onClick={() => handleItemClick('about')}>About KapeBara</li>

              {/* Switch to Admin */}
              <li onClick={() => handleItemClick('admin')}>Demo - Admin Mode</li>
              
              <li className="logout-item" onClick={() => handleItemClick('home')}>Log Out</li>
            </>
          )}

        </ul>
      </div>
    </div>
  );
};

export default PopupMenu;