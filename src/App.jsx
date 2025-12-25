import Register from './components/Register';
import Favicon from './components/Favicon';
import './App.css'; // Add this to keep your old styles

function App() {
    return (
        <div className="App">
            <Favicon />
            <Register />
        </div>
    );
}

export default App;