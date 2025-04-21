
import { CartItem as CartItemType } from "@/types/product";
import { Button } from "@/components/ui/button";
import { useCart } from "@/context/CartContext";
import { Trash, Plus, Minus } from "lucide-react";

interface CartItemProps {
  item: CartItemType;
}

export function CartItem({ item }: CartItemProps) {
  const { updateQuantity, removeFromCart } = useCart();
  const { product, quantity } = item;

  const handleIncrement = () => {
    if (quantity < product.stock) {
      updateQuantity(product.id, quantity + 1);
    }
  };

  const handleDecrement = () => {
    if (quantity > 1) {
      updateQuantity(product.id, quantity - 1);
    }
  };

  return (
    <div className="flex items-center gap-4 py-4 border-b">
      <div className="h-20 w-20 overflow-hidden rounded-md bg-gray-100">
        <img
          src={product.image}
          alt={product.name}
          className="h-full w-full object-cover"
        />
      </div>
      <div className="flex flex-1 flex-col">
        <div className="flex justify-between">
          <h4 className="font-medium">{product.name}</h4>
          <p className="font-medium">${(product.price * quantity).toFixed(2)}</p>
        </div>
        <p className="text-sm text-gray-500">${product.price.toFixed(2)} each</p>
        <div className="mt-2 flex items-center justify-between">
          <div className="flex items-center gap-2">
            <Button 
              variant="outline" 
              size="icon" 
              className="h-8 w-8 rounded-full"
              onClick={handleDecrement}
              disabled={quantity <= 1}
            >
              <Minus className="h-3 w-3" />
            </Button>
            <span className="w-8 text-center">{quantity}</span>
            <Button 
              variant="outline" 
              size="icon" 
              className="h-8 w-8 rounded-full"
              onClick={handleIncrement}
              disabled={quantity >= product.stock}
            >
              <Plus className="h-3 w-3" />
            </Button>
          </div>
          <Button 
            variant="ghost" 
            size="icon" 
            className="text-red-500 hover:text-red-600 hover:bg-red-50"
            onClick={() => removeFromCart(product.id)}
          >
            <Trash className="h-4 w-4" />
          </Button>
        </div>
      </div>
    </div>
  );
}
