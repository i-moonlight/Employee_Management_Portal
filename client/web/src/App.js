import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { ProtectedRoute } from './router/ProtectedRoute';
import DashboardAdminPage from './pages/admin/DashboardAdminPage';
import HomePage from './pages/HomePage';
import SigninPage from './auth/SigninPage';
import SignupPage from './auth/SignupPage';
import './App.css'
import Header from './components/Header';
import { Container } from 'react-bootstrap';
import Footer from './components/Footer';


const App = () => {
    return (
        <>
            <Header/>
            <main>
                <Container>
                    <Routes>
                        <Route path="/" element={<HomePage/>}/>
                        <Route path="/signin" element={<SigninPage/>}/>
                        <Route path="/signup" element={<SignupPage/>}/>
                        <Route path="/admin/dashboard" element={
                            <ProtectedRoute>
                                <DashboardAdminPage/>
                            </ProtectedRoute>
                        }/>
                    </Routes>
                </Container>
            </main>
            <Footer/>
        </>
    );
}

export default App;
