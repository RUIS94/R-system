
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardFooter, CardHeader } from "@/components/ui/card";
import { Product } from "@/types/product";
import { useCart } from "@/context/CartContext";
import { Link } from "react-router-dom";

interface ProductCardProps {
  product: Product;
}

export function ProductCard({ product }: ProductCardProps) {
  const { addToCart } = useCart();

  return (
    <Card className="overflow-hidden transition-all duration-300 hover:shadow-lg">
      <Link to={`/products/${product.id}`}>
        <div className="aspect-square overflow-hidden">
          <img
            src={product.image}
            alt={product.name}
            className="h-full w-full object-cover transition-transform duration-300 hover:scale-105"
          />
        </div>
        <CardHeader className="p-4">
          <div className="flex justify-between">
            <h3 className="font-medium">{product.name}</h3>
            <p className="font-bold text-blue-600">${product.price.toFixed(2)}</p>
          </div>
        </CardHeader>
      </Link>
      <CardContent className="p-4 pt-0">
        <p className="text-sm text-gray-500 line-clamp-2">{product.description}</p>
      </CardContent>
      <CardFooter className="p-4">
        <Button 
          className="w-full bg-blue-600 hover:bg-blue-700" 
          onClick={(e) => {
            e.preventDefault();
            addToCart(product);
          }}
        >
          Add to Cart
        </Button>
      </CardFooter>
    </Card>
  );
}
