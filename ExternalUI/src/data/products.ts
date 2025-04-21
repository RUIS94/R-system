
import { Product } from '../types/product';

export const products: Product[] = [
  {
    id: '1',
    name: 'Modern Desk Lamp',
    description: 'A sleek, adjustable desk lamp with touch controls and multiple brightness settings.',
    price: 49.99,
    image: '/placeholder.svg',
    category: 'Home',
    featured: true,
    stock: 15
  },
  {
    id: '2',
    name: 'Wireless Earbuds',
    description: 'High-quality wireless earbuds with noise cancellation and long battery life.',
    price: 89.99,
    image: '/placeholder.svg',
    category: 'Electronics',
    featured: true,
    stock: 10
  },
  {
    id: '3',
    name: 'Leather Wallet',
    description: 'Handcrafted genuine leather wallet with RFID protection.',
    price: 39.99,
    image: '/placeholder.svg',
    category: 'Accessories',
    featured: true,
    stock: 20
  },
  {
    id: '4',
    name: 'Smart Watch',
    description: 'Feature-rich smartwatch with health tracking and notification support.',
    price: 129.99,
    image: '/placeholder.svg',
    category: 'Electronics',
    featured: false,
    stock: 8
  },
  {
    id: '5',
    name: 'Cotton T-Shirt',
    description: 'Soft, breathable cotton t-shirt available in multiple colors.',
    price: 19.99,
    image: '/placeholder.svg',
    category: 'Clothing',
    featured: false,
    stock: 50
  },
  {
    id: '6',
    name: 'Stainless Steel Water Bottle',
    description: 'Double-walled insulated water bottle that keeps drinks cold for 24 hours.',
    price: 24.99,
    image: '/placeholder.svg',
    category: 'Kitchen',
    featured: true,
    stock: 30
  },
  {
    id: '7',
    name: 'Wireless Charging Pad',
    description: 'Fast wireless charging pad compatible with all Qi-enabled devices.',
    price: 29.99,
    image: '/placeholder.svg',
    category: 'Electronics',
    featured: false,
    stock: 25
  },
  {
    id: '8',
    name: 'Ceramic Coffee Mug',
    description: 'Artisan ceramic coffee mug with unique glazed design.',
    price: 14.99,
    image: '/placeholder.svg',
    category: 'Kitchen',
    featured: false,
    stock: 40
  }
];

export const categories = [...new Set(products.map(product => product.category))];
