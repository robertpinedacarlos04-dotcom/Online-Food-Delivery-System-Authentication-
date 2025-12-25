import { useState } from 'react';

function App() {
  const [currentPage, setCurrentPage] = useState('login');
  return (
    <>
      {currentPage === 'login' && <LoginPage setCurrentPage={setCurrentPage} />}
      {currentPage === 'forgotpassword' && <ForgotPasswordPage />}
    </>
  );
}

// LOGIN PAGE
function LoginPage({ setCurrentPage }) {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Login submitted', { email, password });
  };

  return (
    <div style={styles.body}>
      <div style={styles.container}>
        {/* LEFT */}
        <div style={styles.leftSection}>
          <img src="/kapebara logo.png" alt="Kapebara Logo" style={styles.logo} />
          <img src="/capybara.png" alt="Capybara sipping drink" style={styles.capybaraImage} />
          <p style={styles.signupText}>
            Don't have an account yet?{' '}
            <span style={styles.signupLink} onClick={() => alert('Sign up clicked!')}>
              Sign up here
            </span>
          </p>
        </div>

        {/* RIGHT */}
        <div style={styles.rightSection}>
          <h1 style={styles.header}>LOG IN</h1>
          <form style={styles.formContainer} onSubmit={handleSubmit}>
            <p style={styles.formInfo}>You are logging in as a Customer.</p>

            <div style={styles.formGroup}>
              <label htmlFor="email" style={styles.label}>Email Address</label>
              <input
                type="email"
                id="email"
                placeholder="Enter your email address..."
                required
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
                required
                style={styles.input}
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>

            <button type="submit" style={styles.submitBtn}>Submit</button>

            <div style={styles.forgotPassword}>
              <span
                style={styles.link}
                onClick={() => setCurrentPage('forgotpassword')}
              >
                Forgot Password
              </span>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}

// FORGOT PASSWORD PAGE
function ForgotPasswordPage() {
  const [formData, setFormData] = useState({
    email: '',
    newPassword: '',
    confirmPassword: ''
  });

  const handleChange = (field, value) => {
    setFormData({ ...formData, [field]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (formData.newPassword !== formData.confirmPassword) {
      alert('Passwords do not match!');
      return;
    }
    console.log('Password reset submitted', formData);
    alert('Password reset successful!');
  };

  return (
    <div style={styles.body}>
      <div style={styles.container}>
        {/* LEFT */}
        <div style={styles.leftSection}>
          <img src="/kapebara logo.png" alt="Kapebara Logo" style={styles.logo} />
          <img src="/capybara.png" alt="Capybara sipping drink" style={styles.capybaraImage} />
        </div>

        {/* RIGHT */}
        <div style={styles.rightSection}>
          <h1 style={styles.forgotHeader}>FORGOT PASSWORD</h1>
          <form style={styles.formContainer} onSubmit={handleSubmit}>
            <div style={styles.formGroup}>
              <label htmlFor="forgot-email" style={styles.label}>Email</label>
              <input
                type="email"
                id="forgot-email"
                placeholder="Enter your email..."
                required
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
                required
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
                required
                style={styles.input}
                value={formData.confirmPassword}
                onChange={(e) => handleChange('confirmPassword', e.target.value)}
              />
            </div>

            <button type="submit" style={styles.submitBtn}>Submit</button>
          </form>
        </div>
      </div>
    </div>
  );
}

// STYLES
const styles = {
  body: {
    fontFamily: "'DM Sans', sans-serif",
    backgroundColor: 'white',
    minHeight: '100vh',
    width: '100vw',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    margin: 0,
    padding: 0,
  },

  container: {
    display: 'flex',
    width: '100%',
    height: '100vh',
  },

  leftSection: {
    flex: 1,
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    textAlign: 'center',
    padding: '40px',
  },

  rightSection: {
    flex: 1,
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'center',
    alignItems: 'center',
    padding: '40px',
  },

  logo: {
    width: '280px',
    marginBottom: '40px',
  },

  capybaraImage: {
    width: '400px',
    maxWidth: '90%',
    marginBottom: '20px',
  },

  signupText: {
    fontSize: '18px',
    color: '#3d3d3d',
    fontWeight: 700,
  },

  signupLink: {
    color: '#6b6b6b',
    textDecoration: 'none',
    fontWeight: 700,
    cursor: 'pointer',
    transition: 'all 0.2s',
  },

  header: {
    fontSize: '48px',
    fontWeight: 700,
    color: '#2d2d2d',
    marginBottom: '60px',
    letterSpacing: '-0.5px',
    whiteSpace: 'nowrap',
  },

  forgotHeader: {
    fontSize: '48px',
    fontWeight: 700,
    color: '#2d2d2d',
    marginBottom: '60px',
    letterSpacing: '-0.5px',
    textTransform: 'uppercase',
    whiteSpace: 'nowrap',
  },

  formContainer: {
    width: '100%',
    maxWidth: '420px',
  },

  formInfo: {
    fontSize: '14px',
    color: '#5a5a5a',
    marginBottom: '30px',
    fontWeight: 400,
    textAlign: 'center',
  },

  formGroup: {
    marginBottom: '24px',
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
    width: '45%',
    padding: '8px',
    backgroundColor: '#3d3d3d',
    color: 'white',
    border: 'none',
    borderRadius: '10px',
    fontSize: '12px',
    fontWeight: 500,
    fontFamily: "'DM Sans', sans-serif",
    cursor: 'pointer',
    margin: '50px auto 0 auto',
    display: 'block',
  },

  forgotPassword: {
    textAlign: 'center',
    marginTop: '50px',
  },

  link: {
    color: '#3d3d3d',
    textDecoration: 'underline',
    fontSize: '15px',
    fontWeight: 700,
    cursor: 'pointer',
  },
};

export default App;
