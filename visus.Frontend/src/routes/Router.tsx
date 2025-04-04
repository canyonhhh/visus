import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Navbar from "../components/Navbar";
import Home from "../pages/Home";
import Contact from "../pages/Contact";
import Login from "../pages/Login";
import RegisterPage from "../pages/RegisterPage";

const AppRouter = () => {
    return (
        <Router>
            <Navbar />
            <div >
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<RegisterPage />} />
                    <Route path="/contact" element={<Contact />} />
                </Routes>
            </div>
        </Router>
    );
};

export default AppRouter;
