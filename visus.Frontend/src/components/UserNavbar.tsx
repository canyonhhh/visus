import { useState } from "react";
import { Link } from "react-router-dom";
import { Menu, X } from "lucide-react"; // Icons for mobile menu
import { useLogout } from "../api/auth"; // Import the logout function
const UserNavbar = () => {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const logout = useLogout();
    return (
        <nav className="bg-primary shadow-md fixed w-full top-0 z-50">
            <div className="max-w-7xl mx-auto px-4 py-4 flex justify-between items-center">
                {/* Logo */}
                <h1 className="text-2xl font-bold">
                    <Link to="/home" >Visus</Link>
                </h1>

                {/* Desktop Menu */}
                <ul className="hidden md:flex space-x-6">
                    <li><Link to="/statistics" className="hover:text-blue-500">Statistika</Link></li>
                    <li><Link to="/analytics" className="hover:text-blue-500">Analitika</Link></li>
                    <li><Link to="/qrcode" className="hover:text-blue-500">QR Kodas</Link></li>
                    <li><Link to="/participants" className="hover:text-blue-500">Lankytojai</Link></li>
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
                <>
                <ul className="md:hidden bg-white text-center py-4 space-y-4">
                    <li><Link to="/statistics" className="hover:text-blue-500">Statistika</Link></li>
                    <li><Link to="/analytics" className="hover:text-blue-500">Analitika</Link></li>
                    <li><Link to="/qrcode" className="hover:text-blue-500">QR Kodas</Link></li>
                    <li><Link to="/participants" className="hover:text-blue-500">Lankytojai</Link></li>
                    <button
                        onClick={logout}
                        className="text-black px-4 py-2 rounded-md hover:bg-blue-600 transition-colors hover:cursor-pointer hover:text-white"
                    >Log Out</button>
                </ul>
                
                </>
                )}
            
        </nav>
    );
};

export default UserNavbar;
