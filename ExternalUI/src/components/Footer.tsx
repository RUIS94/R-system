
import { Link } from "react-router-dom";

export function Footer() {
  const currentYear = new Date().getFullYear();
  
  return (
    <footer className="bg-gray-800 text-white py-8">
      <div className="container mx-auto px-4">
        <div className="flex flex-col md:flex-row justify-between">
          <div className="mb-6 md:mb-0">
            <h3 className="text-xl font-bold mb-2">ShopCraft</h3>
            <p className="text-gray-300">Quality products for every need</p>
          </div>
          <div className="grid grid-cols-2 md:grid-cols-3 gap-8">
            <div>
              <h4 className="font-medium mb-2">Shop</h4>
              <ul className="space-y-1">
                <li><Link to="/products" className="text-gray-300 hover:text-white">All Products</Link></li>
                <li><Link to="/products?category=Electronics" className="text-gray-300 hover:text-white">Electronics</Link></li>
                <li><Link to="/products?category=Clothing" className="text-gray-300 hover:text-white">Clothing</Link></li>
              </ul>
            </div>
            <div>
              <h4 className="font-medium mb-2">Company</h4>
              <ul className="space-y-1">
                <li><Link to="/" className="text-gray-300 hover:text-white">About Us</Link></li>
                <li><Link to="/" className="text-gray-300 hover:text-white">Contact</Link></li>
                <li><Link to="/" className="text-gray-300 hover:text-white">Careers</Link></li>
              </ul>
            </div>
            <div>
              <h4 className="font-medium mb-2">Support</h4>
              <ul className="space-y-1">
                <li><Link to="/" className="text-gray-300 hover:text-white">FAQ</Link></li>
                <li><Link to="/" className="text-gray-300 hover:text-white">Shipping</Link></li>
                <li><Link to="/" className="text-gray-300 hover:text-white">Returns</Link></li>
              </ul>
            </div>
          </div>
        </div>
        <div className="mt-8 pt-6 border-t border-gray-700 text-center text-gray-400 text-sm">
          © {currentYear} ShopCraft. All rights reserved.
        </div>
      </div>
    </footer>
  );
}
