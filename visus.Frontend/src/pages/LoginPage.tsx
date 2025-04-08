import Login from "../components/Login"
import PublicNavbar from "../components/PublicNavbar"
const LoginPage = () => {
    return (
        <>
            <PublicNavbar/>
        <section className="bg-[#EEEEEE] py-16 text-center min-h-screen">
            <h1 className="text-4xl font-bold">Login page</h1>
            <Login />
        </section>
        </>
            );
};

export default LoginPage;
