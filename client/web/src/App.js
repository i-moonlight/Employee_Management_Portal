import './App.css';
import DashboardAdminPage from './pages/admin/DashboardAdminPage';
import { Route, Routes } from 'react-router-dom';

const App = () => {
    return (
        <Routes>
            <Route path="/admin/dashboard" element={<DashboardAdminPage/>}/>
        </Routes>
    )
}

export default App;
