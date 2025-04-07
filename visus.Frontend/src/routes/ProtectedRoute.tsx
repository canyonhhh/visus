import React, { useEffect, useState } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { getToken, decodeToken, isTokenValid, extractUserRole } from '../utils/tokenHelper';

interface ProtectedRouteProps {
    requiredRole?: 'ADMIN' | 'SYSTEM' | 'STAFF'; // Added STAFF role
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ requiredRole }) => {
    const [isValidToken, setIsValidToken] = useState<boolean | null>(null);
    const [userRole, setUserRole] = useState<string | null>(null);

    useEffect(() => {
        const token = getToken();

        if (!token) {
            setIsValidToken(false);
            return;
        }

        const decoded = decodeToken(token);
        if (decoded && isTokenValid(token)) {
            const role = extractUserRole(decoded);
            setUserRole(role);
            setIsValidToken(true);
        } else {
            setIsValidToken(false);
        }
    }, []);

    if (isValidToken === null) return null;
    if (!isValidToken) return <Navigate to="/login" />;
    if (requiredRole && userRole !== requiredRole) return <Navigate to="/" />;

    return <Outlet />;
};

export default ProtectedRoute;
