import { useNavigate } from 'react-router-dom';

interface Login {
    token: string;
    // You can add more properties if needed, like user data
}

export const login = async (email: string, password: string, rememberMe: boolean): Promise<Login> => {
    const response = await fetch("/api/Auth/Login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password, rememberMe }),
    });

    if (!response.ok) {
        throw new Error("Login Failed");
    }
    // fix up the errors according to the message gotten
    const data: Login = await response.json();
    return data;
};

export const useLogout = () => {
    const navigate = useNavigate();

    const logout = () => {
        localStorage.removeItem('token');
        sessionStorage.removeItem('token');
        navigate('/');
    };

    return logout;
};
