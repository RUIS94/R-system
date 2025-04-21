
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Navbar } from "@/components/Navbar";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import { Separator } from "@/components/ui/separator";
import { useCart } from "@/context/CartContext";
import { CartItem } from "@/components/CartItem";
import { CreditCard, ShoppingBag, Truck } from "lucide-react";
import { toast } from "@/components/ui/use-toast";

export default function CheckoutPage() {
  const navigate = useNavigate();
  const { cart, clearCart } = useCart();
  const [loading, setLoading] = useState(false);
  const [paymentMethod, setPaymentMethod] = useState("credit-card");
  
  if (cart.items.length === 0) {
    return (
      <>
        <Navbar />
        <div className="container mx-auto px-4 py-16 text-center">
          <h1 className="text-3xl font-bold mb-4">Your Cart is Empty</h1>
          <p className="mb-8">Add some products to your cart before checking out.</p>
          <Button onClick={() => navigate("/products")}>
            Continue Shopping
          </Button>
        </div>
      </>
    );
  }
  
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    setLoading(true);
    
    // Simulate order processing
    setTimeout(() => {
      setLoading(false);
      toast({
        title: "Order Placed Successfully!",
        description: "Thank you for your purchase.",
      });
      clearCart();
      navigate("/checkout/success");
    }, 1500);
  };
  
  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      
      <main className="container mx-auto px-4 py-8">
        <h1 className="text-3xl font-bold mb-8">Checkout</h1>
        
        <div className="grid md:grid-cols-[1fr,400px] gap-8">
          {/* Checkout Form */}
          <div>
            <form onSubmit={handleSubmit}>
              {/* Customer Information */}
              <div className="bg-white p-6 rounded-lg shadow-md mb-6">
                <h2 className="text-xl font-bold mb-4">Customer Information</h2>
                
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div className="space-y-2">
                    <Label htmlFor="firstName">First Name</Label>
                    <Input id="firstName" required />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor="lastName">Last Name</Label>
                    <Input id="lastName" required />
                  </div>
                </div>
                
                <div className="mt-4 space-y-2">
                  <Label htmlFor="email">Email</Label>
                  <Input id="email" type="email" required />
                </div>
                
                <div className="mt-4 space-y-2">
                  <Label htmlFor="phone">Phone Number</Label>
                  <Input id="phone" type="tel" required />
                </div>
              </div>
              
              {/* Shipping Information */}
              <div className="bg-white p-6 rounded-lg shadow-md mb-6">
                <div className="flex items-center gap-2 mb-4">
                  <Truck className="h-5 w-5" />
                  <h2 className="text-xl font-bold">Shipping Information</h2>
                </div>
                
                <div className="space-y-2">
                  <Label htmlFor="address">Address</Label>
                  <Input id="address" required />
                </div>
                
                <div className="mt-4 grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div className="space-y-2">
                    <Label htmlFor="city">City</Label>
                    <Input id="city" required />
                  </div>
                  
                  <div className="space-y-2">
                    <Label htmlFor="state">State</Label>
                    <Input id="state" required />
                  </div>
                </div>
                
                <div className="mt-4 grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div className="space-y-2">
                    <Label htmlFor="zipCode">Zip Code</Label>
                    <Input id="zipCode" required />
                  </div>
                  
                  <div className="space-y-2">
                    <Label htmlFor="country">Country</Label>
                    <Input id="country" defaultValue="United States" required />
                  </div>
                </div>
              </div>
              
              {/* Payment Information */}
              <div className="bg-white p-6 rounded-lg shadow-md mb-6">
                <div className="flex items-center gap-2 mb-4">
                  <CreditCard className="h-5 w-5" />
                  <h2 className="text-xl font-bold">Payment Method</h2>
                </div>
                
                <RadioGroup 
                  defaultValue="credit-card"
                  value={paymentMethod}
                  onValueChange={setPaymentMethod}
                  className="mb-4"
                >
                  <div className="flex items-center space-x-2 mb-2">
                    <RadioGroupItem value="credit-card" id="credit-card" />
                    <Label htmlFor="credit-card">Credit Card</Label>
                  </div>
                  <div className="flex items-center space-x-2 mb-2">
                    <RadioGroupItem value="paypal" id="paypal" />
                    <Label htmlFor="paypal">PayPal</Label>
                  </div>
                </RadioGroup>
                
                {paymentMethod === "credit-card" && (
                  <div className="space-y-4">
                    <div className="space-y-2">
                      <Label htmlFor="cardNumber">Card Number</Label>
                      <Input id="cardNumber" placeholder="1234 5678 9012 3456" required />
                    </div>
                    
                    <div className="grid grid-cols-2 gap-4">
                      <div className="space-y-2">
                        <Label htmlFor="expiryDate">Expiry Date</Label>
                        <Input id="expiryDate" placeholder="MM/YY" required />
                      </div>
                      <div className="space-y-2">
                        <Label htmlFor="cvc">CVC</Label>
                        <Input id="cvc" placeholder="123" required />
                      </div>
                    </div>
                    
                    <div className="space-y-2">
                      <Label htmlFor="nameOnCard">Name on Card</Label>
                      <Input id="nameOnCard" required />
                    </div>
                  </div>
                )}
              </div>
              
              <Button 
                type="submit" 
                className="w-full bg-blue-600 hover:bg-blue-700"
                disabled={loading}
              >
                {loading ? "Processing..." : "Place Order"}
              </Button>
            </form>
          </div>
          
          {/* Order Summary */}
          <div className="bg-white p-6 rounded-lg shadow-md h-fit">
            <div className="flex items-center gap-2 mb-4">
              <ShoppingBag className="h-5 w-5" />
              <h2 className="text-xl font-bold">Order Summary</h2>
            </div>
            
            <div className="mb-4">
              {cart.items.map((item) => (
                <div key={item.product.id} className="py-3 border-b">
                  <div className="flex justify-between">
                    <div>
                      <p className="font-medium">{item.product.name}</p>
                      <p className="text-sm text-gray-500">Qty: {item.quantity}</p>
                    </div>
                    <p className="font-medium">
                      ${(item.product.price * item.quantity).toFixed(2)}
                    </p>
                  </div>
                </div>
              ))}
            </div>
            
            <Separator className="my-4" />
            
            <div className="space-y-2">
              <div className="flex justify-between">
                <span>Subtotal</span>
                <span>${cart.total.toFixed(2)}</span>
              </div>
              <div className="flex justify-between">
                <span>Shipping</span>
                <span>$5.00</span>
              </div>
              <div className="flex justify-between">
                <span>Tax</span>
                <span>${(cart.total * 0.08).toFixed(2)}</span>
              </div>
            </div>
            
            <Separator className="my-4" />
            
            <div className="flex justify-between text-lg font-bold">
              <span>Total</span>
              <span>${(cart.total + 5 + (cart.total * 0.08)).toFixed(2)}</span>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
}
