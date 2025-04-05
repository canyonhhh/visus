// /src/api/auth.ts

// /src/api/auth.ts

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
