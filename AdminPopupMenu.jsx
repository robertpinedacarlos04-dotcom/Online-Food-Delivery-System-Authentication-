import React, { useEffect, useState } from 'react';
import '../css/AdminPopupMenu.css';

const AdminPopupMenu = ({ isOpen, onClose, navigateTo }) => {
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

  return (
    <div 
      className={`admin-popup-overlay ${isOpen ? 'open' : 'closing'}`} 
      onClick={onClose}
    >
      <div 
        className={`admin-popup-content ${isOpen ? 'open' : 'closing'}`}
        onClick={(e) => e.stopPropagation()} 
      >
        <ul className="admin-popup-list">
          <li onClick={() => handleItemClick('profile')}>Profile</li>
          
          {/*for mobile only */}
          <li className="mobile-only" onClick={() => handleItemClick('orders')}>Orders</li>
          <li className="mobile-only" onClick={() => handleItemClick('delivery')}>Delivery</li>
          
          <li onClick={() => handleItemClick('manage-history')}>Manage History</li>
          <li onClick={() => handleItemClick('transaction-history')}>Transaction History</li>
          
          <li className="logout-item" onClick={() => handleItemClick('logout')}>Log Out</li>
        </ul>
      </div>
    </div>
  );
};

export default AdminPopupMenu;