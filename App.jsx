import React, { useState, useEffect } from 'react';
import './App.css';
import logo from './assets/logo.png'; 

import Navbar from './components/Navbar';
import MobileMenu from './components/MobileMenu'; 
import ProductModal from './components/ProductModal';

// 👇 Data Import
import { PRODUCTS } from './data'; 

import { api } from './services/api'; 
import Home from './pages/Home';
import Menu from './pages/Menu'; 
import AdminMenu from './pages/AdminMenu'; 

// 👇 FIX: Import the CreateAdminAcc component
import CreateAdminAcc from './g1/createadminacc'; 

const CATEGORY_LABELS = {
  classic: 'Classic Coffee Series',
  frappe: 'Frappe',
  latte: 'Latte',
  specialty: 'Specialty Drinks',
  baked: 'Cupcakes & Baked Goods',
  snacks: 'Snacks'
};

function App() {
  const [currentPage, setCurrentPage] = useState('home'); 
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState(null);
  
  // 👇 FIX: State to show/hide the Create Admin Modal
  const [showCreateAdmin, setShowCreateAdmin] = useState(false);

  const [menuItems, setMenuItems] = useState(PRODUCTS);
  const [loading, setLoading] = useState(false);

  const loadData = async () => {
    setLoading(true);
    const data = await api.fetchProducts();
    if (data) {
      setMenuItems(data);
    }
    setLoading(false);
  };

  useEffect(() => {
    loadData();
  }, []);

  const handleRefreshData = () => {
    loadData();
  };

  const handleViewProduct = (product) => {
    let categoryName = 'Signature Series';
    if (product.category) {
       categoryName = product.category;
    } else {
       for (const [key, items] of Object.entries(menuItems)) {
          if (key !== 'bestsellers' && Array.isArray(items)) {
             if (items.some(item => item.id === product.id)) {
                categoryName = CATEGORY_LABELS[key] || key; 
                break;
             }
          }
       }
    }
    setSelectedProduct({ ...product, category: categoryName });
  };

  const handleCloseModal = () => setSelectedProduct(null);
  
  const getRelatedItems = () => {
    if (!selectedProduct) return [];
    const categoryKeys = Object.keys(menuItems).filter(key => key !== 'bestsellers');
    for (const key of categoryKeys) {
      const categoryItems = menuItems[key];
      if (Array.isArray(categoryItems) && categoryItems.some(item => item.id === selectedProduct.id)) {
        return categoryItems; 
      }
    }
    return []; 
  };

  const navigateTo = (page, e) => {
    if (e) e.preventDefault();
    
    // 👇 FIX: Intercept 'create_admin' to show modal instead of changing page
    if (page === 'create_admin') {
      setShowCreateAdmin(true);
    } else {
      setCurrentPage(page);
      window.scrollTo(0, 0); 
    }
  };

  const renderContent = () => {
    switch (currentPage) {
      case 'home':
        return (
          <Home 
            products={menuItems} 
            onViewProduct={handleViewProduct} 
            onOrderNow={(e) => navigateTo('menu', e)} 
          />
        );
      case 'admin':
        return (
          <AdminMenu 
            products={menuItems} 
            onSave={handleRefreshData} 
            onViewProduct={handleViewProduct} 
          />
        );
      case 'menu':
        return (
          <Menu 
            products={menuItems}
            onViewProduct={handleViewProduct} 
          />
        );
      default:
        return <Menu products={menuItems} onViewProduct={handleViewProduct} />;
    }
  };

  return (
    <div className="app-container">
      <Navbar 
        currentPage={currentPage}
        navigateTo={navigateTo}
        onOpenMobileMenu={() => setIsMobileMenuOpen(true)}
      />

      <MobileMenu 
        isOpen={isMobileMenuOpen} 
        onClose={() => setIsMobileMenuOpen(false)}
        navigateTo={navigateTo}
      />

      {renderContent()}

      <ProductModal 
        product={selectedProduct} 
        onClose={handleCloseModal}
        relatedItems={getRelatedItems()} 
        onViewProduct={handleViewProduct}
      />

      {/* 👇 FIX: Render the CreateAdminAcc modal if state is true */}
      {showCreateAdmin && (
        <CreateAdminAcc onClose={() => setShowCreateAdmin(false)} />
      )}

      <footer>
        <p>temporary footer lang po ito</p>
      </footer>
    </div>
  );
}

export default App;