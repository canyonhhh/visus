import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const ProtectedRoute: React.FC = () => {
    // Replace this with your actual auth logic, for example, context or localStorage
    const auth = localStorage.getItem('token');  // Checking for token in localStorage

    // TODO: implement some smart authentication for the basic login
    
    // If authorized, return the Outlet (The elements inside) to render child elements
    // If not, return Navigate to redirect to the login page
    return auth ? <Outlet /> : <Navigate to="/login" />;
};

export default ProtectedRoute;
