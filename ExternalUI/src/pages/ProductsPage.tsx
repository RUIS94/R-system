
import { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import { Navbar } from "@/components/Navbar";
import { ProductCard } from "@/components/ProductCard";
import { CategoryFilter } from "@/components/CategoryFilter";
import { products, categories } from "@/data/products";
import { Input } from "@/components/ui/input";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";

export default function ProductsPage() {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const categoryParam = queryParams.get('category');
  
  const [selectedCategory, setSelectedCategory] = useState<string | null>(categoryParam);
  const [searchQuery, setSearchQuery] = useState<string>('');
  const [sortBy, setSortBy] = useState<string>("featured");
  
  const handleCategorySelect = (category: string | null) => {
    setSelectedCategory(category);
  };
  
  const filteredProducts = products.filter(product => {
    const matchesCategory = selectedCategory ? product.category === selectedCategory : true;
    const matchesSearch = product.name.toLowerCase().includes(searchQuery.toLowerCase()) || 
                          product.description.toLowerCase().includes(searchQuery.toLowerCase());
    return matchesCategory && matchesSearch;
  });
  
  const sortedProducts = [...filteredProducts].sort((a, b) => {
    switch (sortBy) {
      case "price-low":
        return a.price - b.price;
      case "price-high":
        return b.price - a.price;
      case "name":
        return a.name.localeCompare(b.name);
      default: // featured
        return (b.featured ? 1 : 0) - (a.featured ? 1 : 0);
    }
  });
  
  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      
      <main className="container mx-auto px-4 py-8">
        <h1 className="text-3xl font-bold mb-8">All Products</h1>
        
        <div className="mb-8 grid grid-cols-1 md:grid-cols-[2fr,1fr] gap-4">
          <Input
            type="text"
            placeholder="Search products..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="w-full"
          />
          
          <Select onValueChange={setSortBy} defaultValue={sortBy}>
            <SelectTrigger>
              <SelectValue placeholder="Sort by" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="featured">Featured</SelectItem>
              <SelectItem value="name">Name</SelectItem>
              <SelectItem value="price-low">Price: Low to High</SelectItem>
              <SelectItem value="price-high">Price: High to Low</SelectItem>
            </SelectContent>
          </Select>
        </div>
        
        <CategoryFilter 
          categories={categories} 
          selectedCategory={selectedCategory} 
          onSelectCategory={handleCategorySelect} 
        />
        
        {sortedProducts.length > 0 ? (
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
            {sortedProducts.map(product => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>
        ) : (
          <div className="text-center py-16">
            <p className="text-lg text-gray-500">No products found.</p>
          </div>
        )}
      </main>
      
      {/* Footer (same as HomePage) */}
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
