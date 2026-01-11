import React, { useState, useEffect } from "react";
import "./createadminacc.css"; 
import mascot from "./mascot.png";

const CreateAdminAcc = ({ onClose }) => {
    const [showRider, setShowRider] = useState(false);

    const [adminData, setAdminData] = useState({
        name: "",
        email: "",
        password: "",
        confirmPassword: "",
    });

    const [riderData, setRiderData] = useState({
        name: "",
        email: "",
        password: "",
        confirmPassword: "",
        motorcycleModel: "",
        plateNumber: "",
    });

    // Close on Escape key
    useEffect(() => {
        const esc = (e) => e.key === "Escape" && onClose();
        window.addEventListener("keydown", esc);
        return () => window.removeEventListener("keydown", esc);
    }, [onClose]);

    const handleAdminChange = (e) =>
        setAdminData({ ...adminData, [e.target.name]: e.target.value });

    const handleRiderChange = (e) =>
        setRiderData({ ...riderData, [e.target.name]: e.target.value });

    const submitAdmin = (e) => {
        e.preventDefault();
        if (adminData.password !== adminData.confirmPassword) {
            alert("Admin passwords do not match");
            return;
        }
        alert("Admin account created");
        setAdminData({ name: "", email: "", password: "", confirmPassword: "" });
        onClose(); 
    };

    const submitRider = (e) => {
        e.preventDefault();
        if (riderData.password !== riderData.confirmPassword) {
            alert("Rider passwords do not match");
            return;
        }
        alert("Rider account created");
        setShowRider(false);
    };

    return (
        // Wrapper with new class name to avoid conflicts
        <div className="ca-modal-overlay">
            
            {/* Main Content */}
            <div className="ca-admin-container">
                
                {/* Close Button ("X") */}
                <button className="ca-main-close" onClick={onClose}>
                    &times;
                </button>

                <button className="rider-hover-btn" onClick={() => setShowRider(true)}>
                    Rider Accounts
                </button>

                <h1>Admin Account Management</h1>

                <img src={mascot} alt="Mascot" className="mascot-img" />

                {/* ADMIN FORM */}
                <div className="ca-account-card">
                    <h2>Create Admin Account</h2>
                    <form onSubmit={submitAdmin}>
                        <input name="name" placeholder="Full Name" value={adminData.name} onChange={handleAdminChange} required />
                        <input name="email" type="email" placeholder="Email" value={adminData.email} onChange={handleAdminChange} required />
                        <input name="password" type="password" placeholder="Password" value={adminData.password} onChange={handleAdminChange} required />
                        <input name="confirmPassword" type="password" placeholder="Confirm Password" value={adminData.confirmPassword} onChange={handleAdminChange} required />
                        <button type="submit">Create Admin</button>
                    </form>
                </div>

                {/* RIDER MODAL (Nested) */}
                {showRider && (
                    <div className="ca-modal-overlay" onClick={() => setShowRider(false)} style={{zIndex: 11001}}>
                        <div className="ca-modal" onClick={(e) => e.stopPropagation()}>
                            <button className="ca-modal-close" onClick={() => setShowRider(false)}>
                                &times;
                            </button>

                            <div className="ca-account-card">
                                <h2>Create Rider Account</h2>
                                <form onSubmit={submitRider}>
                                    <input name="name" placeholder="Full Name" value={riderData.name} onChange={handleRiderChange} required />
                                    <input name="email" type="email" placeholder="Email" value={riderData.email} onChange={handleRiderChange} required />
                                    <input name="password" type="password" placeholder="Password" value={riderData.password} onChange={handleRiderChange} required />
                                    <input name="confirmPassword" type="password" placeholder="Confirm Password" value={riderData.confirmPassword} onChange={handleRiderChange} required />
                                    <input name="motorcycleModel" placeholder="Motorcycle Model" value={riderData.motorcycleModel} onChange={handleRiderChange} required />
                                    <input name="plateNumber" placeholder="Plate Number" value={riderData.plateNumber} onChange={handleRiderChange} required />
                                    <button type="submit">Create Rider</button>
                                </form>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
};

export default CreateAdminAcc;