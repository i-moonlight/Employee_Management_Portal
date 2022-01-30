import React from 'react';
import { Route, Routes } from 'react-router';
import './App.css';
import HomePage from "./pages/home.page";
import SigninPage from "./auth/SigninPage";
import SignupPage from "./auth/SignupPage";
import { ProtectedRoute } from "./router/ProtectedRoute";
import AdminPage from "./pages/admin/AdminPage";

const App = () => {
  return (
     <>
       <Routes>
         <Route path="/" element={<HomePage/>}/>
         <Route path="/signin" element={<SigninPage/>}/>
         <Route path="/signup" element={<SignupPage/>}/>

         <Route path="/admin/dashboard" element={
           <ProtectedRoute userRole={undefined}>
             <AdminPage/>
           </ProtectedRoute>
         }
         />
       </Routes>
     </>
  );
}

export default App
