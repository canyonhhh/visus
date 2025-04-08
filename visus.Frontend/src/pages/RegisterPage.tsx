import Register from "../components/Register"
import PublicNavbar from "../components/PublicNavbar"
const RegisterPage = () => {
    return (
        <>
        <PublicNavbar/>
        <section className="bg-[#EEEEEE] py-16 text-center min-h-screen">
            <h1 className="text-4xl font-bold">Register page</h1>
            <Register />
        </section>
        </>
            );
};

export default RegisterPage;
