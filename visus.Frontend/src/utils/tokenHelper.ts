import { jwtDecode } from 'jwt-decode';

export interface DecodedToken {
    exp: number;
    [key: string]: any;
}

export function getToken(): string | null {
    return localStorage.getItem("token") || sessionStorage.getItem("token");
}

export function decodeToken(token: string): DecodedToken | null {
    try {
        return jwtDecode(token);
    } catch (error) {
        console.error("Failed to decode token", error);
        return null;
    }
}

export function isTokenValid(token: string): boolean {
    const decoded = decodeToken(token);
    if (!decoded || !decoded.exp) return false;
    const currentTime = Date.now() / 1000;
    return decoded.exp > currentTime;
}

export function extractUserRole(decoded: DecodedToken): string | null {
    return decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;
}

export function getUserRole(): string | null {
    const token = getToken();
    if (!token) return null;
    const decoded = decodeToken(token);
    return decoded ? extractUserRole(decoded) : null;
}
