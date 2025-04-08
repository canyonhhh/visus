// /src/components/Login.tsx

import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../api/auth"; // Import the login function
import FeedbackMessage from "./FeedbackMessage";

interface FormData {
    email: string;
    password: string;
    rememberMe: boolean;
}

const Login: React.FC = () => {
    const navigate = useNavigate();

    const [formData, setFormData] = useState<FormData>({
        email: "",
        password: "",
        rememberMe: false,
    });
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const [success, setSuccess] = useState<boolean>(false);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value, type, checked } = e.target;
        setFormData((prevState) => ({
            ...prevState,
            [name]: type === "checkbox" ? checked : value,
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        setLoading(true);
        setError(null);

        try {
            const { token } = await login(formData.email, formData.password, formData.rememberMe);

            if (token) {
                if (formData.rememberMe) {
                    localStorage.setItem("token", token); // persistent storage
                } else {
                    sessionStorage.setItem("token", token); // temporary storage
                }
            } else {
                throw new Error("Token not found in response");
            }
            setSuccess(true);
            navigate("/home");  // Go to the home page
            
        } catch (error) {
            setError(error instanceof Error ? error.message : "Something went wrong");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="max-w-sm mx-auto my-4 p-4 bg-white shadow-md rounded">
            <h2 className="text-xl font-bold mb-4">Login</h2>
            {success && (
                <div className="mb-4">
                    <FeedbackMessage type="success" message={"Log in was succesfull!"} />
                </div>
            )}
            {error && (
                <div className="mb-4">
                    <FeedbackMessage type="error" message={error} />
                </div>
            )}
            <form onSubmit={handleSubmit}>
                <div className="mb-4">
                    <label htmlFor="email" className="block mb-2">
                        Email
                    </label>
                    <input
                        type="email"
                        id="email"
                        name="email"
                        value={formData.email}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>

                <div className="mb-4">
                    <label htmlFor="password" className="block mb-2">
                        Password
                    </label>
                    <input
                        type="password"
                        id="password"
                        name="password"
                        value={formData.password}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>

                <div className="mb-4 flex items-center">
                    <input
                        type="checkbox"
                        id="rememberMe"
                        name="rememberMe"
                        checked={formData.rememberMe}
                        onChange={handleChange}
                        className="mr-2"
                    />
                    <label htmlFor="rememberMe" className="mb-0">
                        Remember Me
                    </label>
                </div>

                <div className="flex justify-center mb-4">
                    <button
                        type="submit"
                        className="px-4 py-2 bg-blue-500 text-white rounded"
                        disabled={loading}
                    >
                        {loading ? "Logging in..." : "Login"}
                    </button>
                </div>
            </form>
        </div>
    );
};

export default Login;
