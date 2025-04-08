import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { Menu, X } from "lucide-react";
import { useLogout } from "../api/auth";
import { getUserRole } from "../utils/tokenHelper";

const UserNavbar = () => {
    const [isOpen, setIsOpen] = useState(false);
    const [role, setRole] = useState<string | null>(null);
    const logout = useLogout();

    useEffect(() => {
        const userRole = getUserRole();
        setRole(userRole);
    }, []);

    const renderLinks = () => {
        switch (role) {
            case "STAFF":
                return (
                    <>
                        <li><Link to="/statistics" className="hover:text-blue-500">Statistika</Link></li>
                        <li><Link to="/analytics" className="hover:text-blue-500">Analitika</Link></li>
                        <li><Link to="/qrcode" className="hover:text-blue-500">QR Kodas</Link></li>
                        <li><Link to="/participants" className="hover:text-blue-500">Lankytojai</Link></li>
                    </>
                );
            case "ADMIN":
                return (
                    <>
                        <li><Link to="/statistics" className="hover:text-blue-500">Statistika</Link></li>
                        <li><Link to="/analytics" className="hover:text-blue-500">Analitika</Link></li>
                        <li><Link to="/qrcode" className="hover:text-blue-500">QR Kodas</Link></li>
                        <li><Link to="/participants" className="hover:text-blue-500">Lankytojai</Link></li>
                        <li><Link to="/admin" className="hover:text-blue-500">Admin</Link></li>
                    </>
                );
            case "SYSTEM":
                return (
                    <>
                        <li><Link to="/secret" className="hover:text-blue-500">Secret</Link></li>
                    </>
                );
            default:
                return null;
        }
    };

    return (
        <nav className="bg-primary shadow-md fixed w-full top-0 z-50">
            <div className="max-w-7xl mx-auto px-4 py-4 flex justify-between items-center">
                <h1 className="text-2xl font-bold">
                    <Link to="/home">Visus</Link>
                </h1>

                {/* Desktop Menu */}
                <ul className="hidden md:flex space-x-6">
                    {renderLinks()}
                </ul>

                <button
                    onClick={logout}
                    className="hidden md:flex text-white px-4 py-2 rounded-md hover:bg-blue-600 transition-colors hover:cursor-pointer"
                >Log Out</button>

                {/* Mobile Menu Button */}
                <button className="md:hidden" onClick={() => setIsOpen(!isOpen)}>
                    {isOpen ? <X size={24} /> : <Menu size={24} />}
                </button>
            </div>

            {/* Mobile Menu */}
            {isOpen && (
                <ul className="md:hidden bg-white text-center py-4 space-y-4">
                    {renderLinks()}
                    <button
                        onClick={logout}
                        className="text-black px-4 py-2 rounded-md hover:bg-blue-600 transition-colors hover:cursor-pointer hover:text-white"
                    >Log Out</button>
                </ul>
            )}
        </nav>
    );
};

export default UserNavbar;
