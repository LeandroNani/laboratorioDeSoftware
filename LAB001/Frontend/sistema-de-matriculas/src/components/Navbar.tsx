'use client'
import React, { useState } from 'react';

const Navbar: React.FC = () => {
    const [isOpen, setIsOpen] = useState(false);

    return (
        <nav className="bg-zinc-900 shadow-md text-white">
            <div className="container mx-auto px-4 py-3 flex justify-between items-center">
                <div className="text-2xl font-bold ">
                    <a href="#" className=" hover:text-gray-300">Matriculas</a>
                </div>
                <div className="md:hidden">
                    <button
                        onClick={() => setIsOpen(!isOpen)}
                        className="text-gray-600 hover:text-gray-800 focus:outline-none"
                    >
                        <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            {isOpen ? (
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                            ) : (
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
                            )}
                        </svg>
                    </button>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;
