import { useState } from "react";
import { Link } from "react-router-dom";
import { Menu, X } from "lucide-react"; // Icons for mobile menu

const UserNavbar = () => {
    const [isOpen, setIsOpen] = useState<boolean>(false);

    return (
        <nav className="bg-primary shadow-md fixed w-full top-0 z-50">
            <div className="max-w-7xl mx-auto px-4 py-4 flex justify-between items-center">
                {/* Logo */}
                <h1 className="text-2xl font-bold">
                    <Link to="/" >Visus</Link>
                </h1>

                {/* Desktop Menu */}
                <ul className="hidden md:flex space-x-6">
                    <li><Link to="/statistics" className="hover:text-blue-500">Statistika</Link></li>
                    <li><Link to="/analytics" className="hover:text-blue-500">Analitika</Link></li>
                    <li><Link to="/qrcode" className="hover:text-blue-500">QR Kodas</Link></li>
                    <li><Link to="/participants" className="hover:text-blue-500">Lankytojai</Link></li>
                </ul>

                {/* Mobile Menu Button */}
                <button className="md:hidden" onClick={() => setIsOpen(!isOpen)}>
                    {isOpen ? <X size={24} /> : <Menu size={24} />}
                </button>
            </div>

            {/* Mobile Menu */}
            {isOpen && (
                <ul className="md:hidden bg-white text-center py-4 space-y-4">
                    <li><Link to="/statistics" className="hover:text-blue-500">Statistika</Link></li>
                    <li><Link to="/analytics" className="hover:text-blue-500">Analitika</Link></li>
                    <li><Link to="/qrcode" className="hover:text-blue-500">QR Kodas</Link></li>
                    <li><Link to="/participants" className="hover:text-blue-500">Lankytojai</Link></li>
                </ul>
            )}
        </nav>
    );
};

export default UserNavbar;
