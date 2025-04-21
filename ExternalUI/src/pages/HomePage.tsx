
import { useState } from "react";
import { Link } from "react-router-dom";
import { Navbar } from "@/components/Navbar";
import { Footer } from "@/components/Footer";
import { ProductCard } from "@/components/ProductCard";
import { Button } from "@/components/ui/button";
import { products } from "@/data/products";

export default function HomePage() {
  const featuredProducts = products.filter(product => product.featured);
  
  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      
      {/* Hero Section */}
      <section className="bg-blue-600 text-white py-16 md:py-24">
        <div className="container mx-auto px-4 text-center">
          <h1 className="text-4xl md:text-5xl font-bold mb-6">Welcome to ShopCraft</h1>
          <p className="text-xl md:text-2xl mb-8 max-w-2xl mx-auto">
            Discover our curated collection of high-quality products for every need.
          </p>
          <Button asChild size="lg" className="bg-white text-blue-600 hover:bg-gray-100">
            <Link to="/products">Shop Now</Link>
          </Button>
        </div>
      </section>
      
      {/* Featured Products */}
      <section className="py-16">
        <div className="container mx-auto px-4">
          <div className="flex justify-between items-center mb-8">
            <h2 className="text-2xl md:text-3xl font-bold">Featured Products</h2>
            <Link to="/products" className="text-blue-600 hover:underline">
              View all
            </Link>
          </div>
          
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
            {featuredProducts.map(product => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>
        </div>
      </section>
      
      {/* Categories Section */}
      <section className="py-16 bg-gray-100">
        <div className="container mx-auto px-4">
          <h2 className="text-2xl md:text-3xl font-bold mb-8 text-center">Shop by Category</h2>
          
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-4">
            {['Electronics', 'Clothing', 'Home', 'Kitchen'].map(category => (
              <Link 
                key={category}
                to={`/products?category=${category}`}
                className="bg-white rounded-lg shadow-md p-6 text-center transition-transform hover:scale-105"
              >
                <div className="h-24 flex items-center justify-center mb-4">
                  <img src="/placeholder.svg" alt={category} className="h-16 w-16" />
                </div>
                <h3 className="font-medium text-lg">{category}</h3>
              </Link>
            ))}
          </div>
        </div>
      </section>
      
      {/* CTA Banner */}
      <section className="bg-blue-50 py-16">
        <div className="container mx-auto px-4 text-center">
          <h2 className="text-2xl md:text-3xl font-bold mb-4">Ready to Discover More?</h2>
          <p className="text-lg mb-8 max-w-2xl mx-auto">
            Explore our full catalog and find exactly what you're looking for.
          </p>
          <Button asChild size="lg" className="bg-blue-600 hover:bg-blue-700">
            <Link to="/products">Browse All Products</Link>
          </Button>
        </div>
      </section>
      
      <Footer />
    </div>
  );
}
