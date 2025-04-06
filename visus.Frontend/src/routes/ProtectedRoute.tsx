import React, { useEffect, useState } from 'react';
import { Navigate, Outlet, useNavigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';

interface DecodedToken {
    exp: number;
    role: string;
    [key: string]: any;
}

interface ProtectedRouteProps {
    requiredRole?: 'ADMIN' | 'SYSTEM'; // Optional prop to specify the required role for a route
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ requiredRole }) => {
    const [isValidToken, setIsValidToken] = useState<boolean | null>(null);
    const [userRole, setUserRole] = useState<string | null>(null); // Store the role
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token") || sessionStorage.getItem("token");

        if (!token) {
            setIsValidToken(false);
            return;
        }

        try {
            const decoded: DecodedToken = jwtDecode(token);
            const currentTime = Date.now() / 1000;
            const roleClaim = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']; // Extract the role

            setUserRole(roleClaim); // Store the role in state

            if (decoded.exp && decoded.exp > currentTime) {
                setIsValidToken(true);
            } else {
                setIsValidToken(false);
            }
        } catch (err) {
            console.error("Invalid token", err);
            setIsValidToken(false);
        }
    }, []);

    // If no token is found or if token is invalid, redirect to login
    if (isValidToken === null) return null; // Render nothing while checking token

    // If token is invalid, redirect to login page
    if (!isValidToken) {
        return <Navigate to="/login" />;
    }

    // Handle role-based routing
    if (requiredRole && userRole !== requiredRole) {
        // Redirect based on the required role if the role does not match
        if (requiredRole === 'ADMIN') {
            return <Navigate to="/" />;
        } else if (requiredRole === 'SYSTEM') {
            return <Navigate to="/" />;
        }
    }
    
    return <Outlet />;
};

export default ProtectedRoute;
