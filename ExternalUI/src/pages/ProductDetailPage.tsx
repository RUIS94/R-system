
import { useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Navbar } from "@/components/Navbar";
import { Button } from "@/components/ui/button";
import { products } from "@/data/products";
import { useCart } from "@/context/CartContext";
import { Minus, Plus, ShoppingBag } from "lucide-react";

export default function ProductDetailPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { addToCart } = useCart();
  const [quantity, setQuantity] = useState(1);
  
  const product = products.find(p => p.id === id);
  
  if (!product) {
    return (
      <>
        <Navbar />
        <div className="container mx-auto px-4 py-16 text-center">
          <h1 className="text-3xl font-bold mb-4">Product Not Found</h1>
          <p className="mb-8">The product you are looking for does not exist.</p>
          <Button onClick={() => navigate("/products")}>
            Back to Products
          </Button>
        </div>
      </>
    );
  }
  
  const handleIncrement = () => {
    if (quantity < product.stock) {
      setQuantity(quantity + 1);
    }
  };
  
  const handleDecrement = () => {
    if (quantity > 1) {
      setQuantity(quantity - 1);
    }
  };
  
  const handleAddToCart = () => {
    addToCart(product, quantity);
  };
  
  // Find related products (same category, excluding current product)
  const relatedProducts = products
    .filter(p => p.category === product.category && p.id !== product.id)
    .slice(0, 4);
    
  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      
      <main className="container mx-auto px-4 py-8">
        <div className="mb-8">
          <Button variant="ghost" onClick={() => navigate(-1)} className="mb-4">
            ← Back
          </Button>
          
          <div className="bg-white rounded-lg shadow-md overflow-hidden">
            <div className="grid md:grid-cols-2 gap-8 p-6">
              {/* Product Image */}
              <div className="aspect-square bg-gray-100 rounded-md overflow-hidden">
                <img 
                  src={product.image} 
                  alt={product.name} 
                  className="w-full h-full object-contain"
                />
              </div>
              
              {/* Product Details */}
              <div className="flex flex-col">
                <h1 className="text-3xl font-bold mb-2">{product.name}</h1>
                <p className="text-2xl font-bold text-blue-600 mb-4">
                  ${product.price.toFixed(2)}
                </p>
                
                <div className="border-t border-b py-4 my-4">
                  <p className="text-gray-600">{product.description}</p>
                </div>
                
                <div className="mb-6">
                  <p className="mb-2">
                    <span className="font-medium">Category:</span> {product.category}
                  </p>
                  <p className="mb-2">
                    <span className="font-medium">Availability:</span>{" "}
                    {product.stock > 0 ? (
                      <span className="text-green-600">In Stock ({product.stock} left)</span>
                    ) : (
                      <span className="text-red-600">Out of Stock</span>
                    )}
                  </p>
                </div>
                
                <div className="mt-auto">
                  <div className="flex items-center gap-4 mb-4">
                    <span className="font-medium">Quantity:</span>
                    <div className="flex items-center border rounded-md">
                      <Button 
                        variant="ghost" 
                        size="icon" 
                        onClick={handleDecrement}
                        disabled={quantity <= 1}
                        className="h-10 w-10 rounded-none"
                      >
                        <Minus className="h-4 w-4" />
                      </Button>
                      <span className="w-12 text-center">{quantity}</span>
                      <Button 
                        variant="ghost" 
                        size="icon" 
                        onClick={handleIncrement}
                        disabled={quantity >= product.stock}
                        className="h-10 w-10 rounded-none"
                      >
                        <Plus className="h-4 w-4" />
                      </Button>
                    </div>
                  </div>
                  
                  <Button 
                    className="w-full bg-blue-600 hover:bg-blue-700 flex items-center gap-2"
                    onClick={handleAddToCart}
                    disabled={product.stock <= 0}
                  >
                    <ShoppingBag className="h-5 w-5" />
                    Add to Cart
                  </Button>
                </div>
              </div>
            </div>
          </div>
        </div>
        
        {/* Related Products */}
        {relatedProducts.length > 0 && (
          <div className="mt-16">
            <h2 className="text-2xl font-bold mb-6">Related Products</h2>
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-6">
              {relatedProducts.map(relatedProduct => (
                <div 
                  key={relatedProduct.id} 
                  className="bg-white rounded-lg shadow-md overflow-hidden cursor-pointer"
                  onClick={() => navigate(`/products/${relatedProduct.id}`)}
                >
                  <div className="aspect-square overflow-hidden">
                    <img
                      src={relatedProduct.image}
                      alt={relatedProduct.name}
                      className="h-full w-full object-cover"
                    />
                  </div>
                  <div className="p-4">
                    <h3 className="font-medium">{relatedProduct.name}</h3>
                    <p className="font-bold text-blue-600">${relatedProduct.price.toFixed(2)}</p>
                  </div>
                </div>
              ))}
            </div>
          </div>
        )}
      </main>
      
      {/* Footer (Same as other pages) */}
      <footer className="bg-gray-800 text-white py-8 mt-16">
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
                  <li><a href="/products" className="text-gray-300 hover:text-white">All Products</a></li>
                  <li><a href="/products?category=Electronics" className="text-gray-300 hover:text-white">Electronics</a></li>
                  <li><a href="/products?category=Clothing" className="text-gray-300 hover:text-white">Clothing</a></li>
                </ul>
              </div>
              <div>
                <h4 className="font-medium mb-2">Company</h4>
                <ul className="space-y-1">
                  <li><a href="#" className="text-gray-300 hover:text-white">About Us</a></li>
                  <li><a href="#" className="text-gray-300 hover:text-white">Contact</a></li>
                  <li><a href="#" className="text-gray-300 hover:text-white">Careers</a></li>
                </ul>
              </div>
              <div>
                <h4 className="font-medium mb-2">Support</h4>
                <ul className="space-y-1">
                  <li><a href="#" className="text-gray-300 hover:text-white">FAQ</a></li>
                  <li><a href="#" className="text-gray-300 hover:text-white">Shipping</a></li>
                  <li><a href="#" className="text-gray-300 hover:text-white">Returns</a></li>
                </ul>
              </div>
            </div>
          </div>
          <div className="mt-8 pt-6 border-t border-gray-700 text-center text-gray-400 text-sm">
            © {new Date().getFullYear()} ShopCraft. All rights reserved.
          </div>
        </div>
      </footer>
    </div>
  );
}
