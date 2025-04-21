
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { CheckCircle } from "lucide-react";
import { Navbar } from "@/components/Navbar";

export default function CheckoutSuccessPage() {
  const navigate = useNavigate();
  
  // Generate random order number
  const orderNumber = `ORD-${Math.floor(100000 + Math.random() * 900000)}`;
  
  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      
      <main className="container mx-auto px-4 py-16">
        <div className="max-w-md mx-auto bg-white rounded-lg shadow-md p-8 text-center">
          <div className="flex justify-center mb-4">
            <CheckCircle className="h-16 w-16 text-green-500" />
          </div>
          
          <h1 className="text-2xl font-bold mb-2">Order Confirmed!</h1>
          <p className="text-gray-600 mb-6">
            Thank you for your purchase. Your order has been received and is being processed.
          </p>
          
          <div className="bg-gray-50 rounded-md p-4 mb-6">
            <p className="text-sm text-gray-500 mb-1">Order Number</p>
            <p className="font-bold">{orderNumber}</p>
          </div>
          
          <p className="text-sm text-gray-500 mb-8">
            A confirmation email has been sent to your email address.
          </p>
          
          <div className="space-y-4">
            <Button 
              className="w-full bg-blue-600 hover:bg-blue-700" 
              onClick={() => navigate("/")}
            >
              Return to Home
            </Button>
            
            <Button 
              variant="outline" 
              className="w-full" 
              onClick={() => navigate("/products")}
            >
              Continue Shopping
            </Button>
          </div>
        </div>
      </main>
    </div>
  );
}
