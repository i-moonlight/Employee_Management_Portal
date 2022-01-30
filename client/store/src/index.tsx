import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import reportWebVitals from './reportWebVitals';
import './index.css';
import './App.css'
import './bootstrap.min.css'
import 'primereact/resources/themes/lara-light-indigo/theme.css';
import 'primereact/resources/primereact.min.css';
// import 'primeicons/primeicons.css';
// import 'primeflex/primeflex.css';
import { AuthProvider } from './auth/useAuth';
import { BrowserRouter } from "react-router-dom";

const root = ReactDOM.createRoot(document.getElementById("root") as HTMLElement);
root.render(
   <BrowserRouter>
     <AuthProvider>
       <App/>
     </AuthProvider>
   </BrowserRouter>
);

reportWebVitals();
