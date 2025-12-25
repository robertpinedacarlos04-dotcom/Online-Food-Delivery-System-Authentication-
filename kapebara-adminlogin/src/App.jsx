import { useState } from 'react'

function App() {
  const [currentPage, setCurrentPage] = useState('login');
  
  if (currentPage === 'forgotpassword') {
    return <ForgotPasswordPage setCurrentPage={setCurrentPage} />;
  }
  
  return <LoginPage setCurrentPage={setCurrentPage} />;
}

// Login Page Component
function LoginPage({ setCurrentPage }) {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = () => {
    console.log('Form submitted', { email, password });
  };

  return (
    <div style={styles.fullScreen}>
      <div style={styles.loginGrid}>
        <div style={styles.leftColumn}>
          <img src="/kapebara logo.png" alt="Kapebara Logo" style={styles.logoLeft} />
          <img src="/capybara.png" alt="Capybara sipping drink" style={styles.capybaraImage} />
        </div>
        
        <div style={styles.rightColumn}>
          <h1 style={styles.loginHeader}>LOG IN</h1>
          <h2 style={styles.welcomeText}>Welcome back, [Name]!</h2>
          <p style={styles.formInfo}>You are logging in as an Admin.</p>
          
          <div style={styles.loginForm}>
            <div style={styles.formGroup}>
              <label htmlFor="email" style={styles.label}>Email Address</label>
              <input 
                type="email" 
                id="email" 
                placeholder="Enter your email address..." 
                style={styles.input}
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>

            <div style={styles.formGroup}>
              <label htmlFor="password" style={styles.label}>Password</label>
              <input 
                type="password" 
                id="password" 
                placeholder="Enter your password..." 
                style={styles.input}
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>

            <button onClick={handleSubmit} style={styles.submitBtn}>Submit</button>

            <div style={styles.forgotPassword}>
              <span 
                onClick={() => setCurrentPage('forgotpassword')} 
                style={styles.forgotPasswordLink}
              >
                Forgot Password
              </span>
            </div>

            <div style={styles.riderLogin}>
              <p style={styles.riderLoginText}>
                Log in as rider instead? 
                <span style={styles.riderLoginLink}> Click here.</span>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

// Forgot Password Page Component
function ForgotPasswordPage({ setCurrentPage }) {
  const [formData, setFormData] = useState({
    email: '',
    newPassword: '',
    confirmPassword: ''
  });

  const handleChange = (field, value) => {
    setFormData({
      ...formData,
      [field]: value
    });
  };

  const handleSubmit = () => {
    if (formData.newPassword !== formData.confirmPassword) {
      alert('Passwords do not match!');
      return;
    }
    
    console.log('Form submitted', formData);
    alert('Password reset successful!');
  };

  return (
    <div style={styles.fullScreen}>
      <div style={styles.forgotContainer}>
        <img src="/kapebara logo.png" alt="Kapebara Logo" style={styles.logoCenter} />
        <h1 style={styles.pageHeader}>FORGOT PASSWORD</h1>
        
        <div style={styles.forgotForm}>
          <div style={styles.formGroup}>
            <label htmlFor="forgot-email" style={styles.label}>Email</label>
            <input 
              type="email" 
              id="forgot-email"
              placeholder="Enter your email..." 
              style={styles.input}
              value={formData.email}
              onChange={(e) => handleChange('email', e.target.value)}
            />
          </div>

          <div style={styles.formGroup}>
            <label htmlFor="new-password" style={styles.label}>New Password</label>
            <input 
              type="password" 
              id="new-password"
              placeholder="Enter your new password..." 
              style={styles.input}
              value={formData.newPassword}
              onChange={(e) => handleChange('newPassword', e.target.value)}
            />
          </div>

          <div style={styles.formGroup}>
            <label htmlFor="confirm-password" style={styles.label}>Confirm Password</label>
            <input 
              type="password" 
              id="confirm-password"
              placeholder="Confirm your password..." 
              style={styles.input}
              value={formData.confirmPassword}
              onChange={(e) => handleChange('confirmPassword', e.target.value)}
            />
          </div>

          <button onClick={handleSubmit} style={styles.submitBtn}>Submit</button>
          
          <div style={styles.backToLogin}>
            <span 
              onClick={() => setCurrentPage('login')} 
              style={styles.backLink}
            >
              Back to Login
            </span>
          </div>
        </div>
      </div>
    </div>
  );
}

const styles = {
  fullScreen: {
    fontFamily: "'DM Sans', sans-serif",
    backgroundColor: 'white',
    minHeight: '100vh',
    width: '100vw',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    margin: 0,
    padding: 0,
  },

  // Login page - two column grid
  loginGrid: {
    display: 'grid',
    gridTemplateColumns: '1fr 1fr',
    width: '100%',
    height: '100vh',
    alignItems: 'center',
    padding: '0 80px',
    boxSizing: 'border-box',
  },

  leftColumn: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
  },

  rightColumn: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
  },

  logoLeft: {
    width: '280px',
    marginBottom: '40px',
  },

  capybaraImage: {
    width: '400px',
    maxWidth: '100%',
  },

  // Forgot password - centered
  forgotContainer: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    width: '100%',
    maxWidth: '500px',
  },

  logoCenter: {
    width: '300px',
    marginBottom: '30px',
  },

  loginHeader: {
    fontSize: '48px',
    fontWeight: 700,
    color: '#2d2d2d',
    marginBottom: '20px',
    letterSpacing: '-0.5px',
    textAlign: 'center',
    margin: '0 0 20px 0',
  },

  pageHeader: {
    fontSize: '36px',
    fontWeight: 700,
    color: '#2d2d2d',
    marginBottom: '40px',
    letterSpacing: '-0.5px',
    textAlign: 'center',
    margin: '0 0 40px 0',
  },

  welcomeText: {
    fontSize: '24px',
    fontWeight: 700,
    color: '#2d2d2d',
    marginBottom: '8px',
    textAlign: 'center',
    margin: '0 0 8px 0',
  },

  formInfo: {
    fontSize: '14px',
    color: '#5a5a5a',
    marginBottom: '30px',
    fontWeight: 400,
    textAlign: 'center',
    margin: '0 0 30px 0',
  },

  loginForm: {
    width: '100%',
    maxWidth: '420px',
  },

  forgotForm: {
    width: '100%',
    maxWidth: '420px',
  },

  formGroup: {
    width: '100%',
    marginBottom: '20px',
  },

  label: {
    display: 'block',
    fontSize: '14px',
    color: '#3d3d3d',
    marginBottom: '8px',
    fontWeight: 500,
  },

  input: {
    width: '100%',
    padding: '14px 16px',
    border: '1px solid #d0d0d0',
    borderRadius: '8px',
    fontSize: '15px',
    fontFamily: "'DM Sans', sans-serif",
    color: '#3d3d3d',
    backgroundColor: 'white',
    boxSizing: 'border-box',
  },

  submitBtn: {
    width: '140px',
    padding: '12px',
    backgroundColor: '#3d3d3d',
    color: 'white',
    border: 'none',
    borderRadius: '10px',
    fontSize: '14px',
    fontWeight: 500,
    cursor: 'pointer',
    margin: '30px auto 0',
    display: 'block',
  },

  forgotPassword: {
    textAlign: 'center',
    marginTop: '20px',
  },

  forgotPasswordLink: {
    color: '#d0d0d0',
    textDecoration: 'underline',
    fontSize: '14px',
    fontWeight: 500,
    cursor: 'pointer',
  },

  riderLogin: {
    textAlign: 'center',
    marginTop: '30px',
  },

  riderLoginText: {
    fontSize: '16px',
    color: '#3d3d3d',
    fontWeight: 700,
    margin: 0,
  },

  riderLoginLink: {
    color: '#6b6b6b',
    fontWeight: 700,
    cursor: 'pointer',
  },

  backToLogin: {
    textAlign: 'center',
    marginTop: '20px',
  },

  backLink: {
    color: '#6b6b6b',
    textDecoration: 'underline',
    fontSize: '14px',
    fontWeight: 500,
    cursor: 'pointer',
  },
};

export default App;