import { useState } from "react";
import { Link } from "react-router-dom";
import { Menu, X } from "lucide-react"; // Icons for mobile menu

const PublicNavbar = () => {
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
                    <li><Link to="/" className="hover:text-blue-500">Pagrindinis</Link></li>
                    <li><Link to="/register" className="hover:text-blue-500">Registruotis</Link></li>
                    <li><Link to="/login" className="hover:text-blue-500">Prisijungti</Link></li>
                </ul>

                {/* Mobile Menu Button */}
                <button className="md:hidden" onClick={() => setIsOpen(!isOpen)}>
                    {isOpen ? <X size={24} /> : <Menu size={24} />}
                </button>
            </div>

            {/* Mobile Menu */}
            {isOpen && (
                <ul className="md:hidden bg-white text-center py-4 space-y-4">
                    <li><Link to="/" className="hover:text-blue-500">Pagrindinis</Link></li>
                    <li><Link to="/register" className="hover:text-blue-500">Registruotis</Link></li>
                    <li><Link to="/login" className="hover:text-blue-500">Prisijungti</Link></li>
                </ul>
            )}
        </nav>
    );
};

export default PublicNavbar;
