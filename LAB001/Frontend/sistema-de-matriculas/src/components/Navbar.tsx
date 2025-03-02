'use client'
const Navbar: React.FC = () => {
    return (
        <nav className="bg-transparent shadow-md text-white">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 bg-transparent">
                <div className="flex justify-between items-center h-16">
                    {/* Logo */}
                    <a className="text-xl font-bold text-white" href="#">Navbar</a>

                    {/* Mobile Menu Button */}
                    <button className="md:hidden text-gray-600 hover:text-white focus:outline-none" id="menu-button">
                        <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16m-7 6h7" />
                        </svg>
                    </button>

                    {/* Navigation Links */}
                    <div className="hidden md:flex md:items-center md:space-x-6">
                        <a className="text-gray-300 hover:text-white" href="/home">Home</a>
                        <a className="text-gray-300 hover:text-white" href="#">Link</a>
                    </div>
                </div>
            </div>
        </nav>
    )
}

export default Navbar;
