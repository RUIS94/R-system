
export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  image: string;
  category: string;
  featured: boolean;
  stock: number;
}

export interface CartItem {
  product: Product;
  quantity: number;
}
