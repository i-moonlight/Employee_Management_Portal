import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { ProtectedRoute } from './router/ProtectedRoute';
import DashboardAdminPage from './pages/admin/DashboardAdminPage';
import HomePage from './pages/HomePage';
import SigninPage from './auth/SigninPage';
import SignupPage from './auth/SignupPage';
import './App.css'


const App = () => {
    return (
        <Routes>
            <Route path="/" element={<HomePage/>}/>

            <Route path="/signin" element={<SigninPage/>}/>

            <Route path="/signup" element={<SignupPage />} />

            <Route path="/admin/dashboard" element={
                <ProtectedRoute>
                    <DashboardAdminPage/>
                </ProtectedRoute>
            }/>
        </Routes>
    );
}

export default App;
