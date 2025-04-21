
import { Link } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { ShoppingBag } from "lucide-react";
import { useCart } from "@/context/CartContext";
import {
  Sheet,
  SheetContent,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet";
import { CartItem } from "@/components/CartItem";
import { useState } from "react";

export function Navbar() {
  const { cart, clearCart } = useCart();
  const [isCartOpen, setIsCartOpen] = useState(false);
  
  const totalItems = cart.items.reduce((sum, item) => sum + item.quantity, 0);

  return (
    <header className="bg-white border-b border-gray-200 sticky top-0 z-40">
      <div className="container mx-auto px-4 py-4">
        <div className="flex justify-between items-center">
          <Link to="/" className="text-2xl font-bold text-blue-600">ShopCraft</Link>
          
          <div className="flex items-center gap-6">
            <nav className="hidden md:flex space-x-6">
              <Link to="/" className="text-gray-700 hover:text-blue-600 transition-colors">Home</Link>
              <Link to="/products" className="text-gray-700 hover:text-blue-600 transition-colors">Products</Link>
            </nav>
            
            <Sheet open={isCartOpen} onOpenChange={setIsCartOpen}>
              <SheetTrigger asChild>
                <Button variant="outline" size="icon" className="relative">
                  <ShoppingBag className="h-5 w-5" />
                  {totalItems > 0 && (
                    <span className="absolute -top-2 -right-2 h-5 w-5 rounded-full bg-blue-600 text-white text-xs flex items-center justify-center">
                      {totalItems}
                    </span>
                  )}
                </Button>
              </SheetTrigger>
              <SheetContent className="sm:max-w-md">
                <SheetHeader>
                  <SheetTitle>Shopping Cart</SheetTitle>
                </SheetHeader>
                <div className="mt-6 flex flex-col gap-2">
                  {cart.items.length === 0 ? (
                    <p className="text-center py-6 text-gray-500">Your cart is empty</p>
                  ) : (
                    <>
                      <div className="space-y-4">
                        {cart.items.map((item) => (
                          <CartItem key={item.product.id} item={item} />
                        ))}
                      </div>
                      <div className="mt-6 border-t pt-4">
                        <div className="flex justify-between font-bold text-lg">
                          <span>Total</span>
                          <span>${cart.total.toFixed(2)}</span>
                        </div>
                        <div className="mt-6 space-y-3">
                          <Button className="w-full bg-blue-600 hover:bg-blue-700" asChild>
                            <Link to="/checkout" onClick={() => setIsCartOpen(false)}>
                              Checkout
                            </Link>
                          </Button>
                          <Button 
                            variant="outline" 
                            className="w-full" 
                            onClick={clearCart}
                          >
                            Clear Cart
                          </Button>
                        </div>
                      </div>
                    </>
                  )}
                </div>
              </SheetContent>
            </Sheet>
          </div>
        </div>
      </div>
    </header>
  );
}
