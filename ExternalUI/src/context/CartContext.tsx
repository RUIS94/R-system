
import React, { createContext, useContext, useReducer, ReactNode } from 'react';
import { CartItem, Product } from '../types/product';

interface CartState {
  items: CartItem[];
  total: number;
}

type CartAction = 
  | { type: 'ADD_ITEM'; product: Product; quantity?: number }
  | { type: 'REMOVE_ITEM'; productId: string }
  | { type: 'UPDATE_QUANTITY'; productId: string; quantity: number }
  | { type: 'CLEAR_CART' };

const initialState: CartState = {
  items: [],
  total: 0
};

const calculateTotal = (items: CartItem[]): number => {
  return items.reduce((sum, item) => sum + item.product.price * item.quantity, 0);
};

const cartReducer = (state: CartState, action: CartAction): CartState => {
  switch (action.type) {
    case 'ADD_ITEM': {
      const existingItemIndex = state.items.findIndex(item => item.product.id === action.product.id);
      
      if (existingItemIndex >= 0) {
        // Product already in cart, update quantity
        const updatedItems = [...state.items];
        updatedItems[existingItemIndex] = {
          ...updatedItems[existingItemIndex],
          quantity: updatedItems[existingItemIndex].quantity + (action.quantity || 1)
        };
        
        return {
          ...state,
          items: updatedItems,
          total: calculateTotal(updatedItems)
        };
      } else {
        // Add new product to cart
        const newItem: CartItem = {
          product: action.product,
          quantity: action.quantity || 1
        };
        
        const updatedItems = [...state.items, newItem];
        
        return {
          ...state,
          items: updatedItems,
          total: calculateTotal(updatedItems)
        };
      }
    }
    
    case 'REMOVE_ITEM': {
      const updatedItems = state.items.filter(item => item.product.id !== action.productId);
      
      return {
        ...state,
        items: updatedItems,
        total: calculateTotal(updatedItems)
      };
    }
    
    case 'UPDATE_QUANTITY': {
      const updatedItems = state.items.map(item => 
        item.product.id === action.productId 
          ? { ...item, quantity: action.quantity } 
          : item
      );
      
      return {
        ...state,
        items: updatedItems,
        total: calculateTotal(updatedItems)
      };
    }
    
    case 'CLEAR_CART':
      return initialState;
      
    default:
      return state;
  }
};

interface CartContextType {
  cart: CartState;
  addToCart: (product: Product, quantity?: number) => void;
  removeFromCart: (productId: string) => void;
  updateQuantity: (productId: string, quantity: number) => void;
  clearCart: () => void;
}

const CartContext = createContext<CartContextType | undefined>(undefined);

export const CartProvider: React.FC<{children: ReactNode}> = ({ children }) => {
  const [cart, dispatch] = useReducer(cartReducer, initialState);
  
  const addToCart = (product: Product, quantity?: number) => {
    dispatch({ type: 'ADD_ITEM', product, quantity });
  };
  
  const removeFromCart = (productId: string) => {
    dispatch({ type: 'REMOVE_ITEM', productId });
  };
  
  const updateQuantity = (productId: string, quantity: number) => {
    dispatch({ type: 'UPDATE_QUANTITY', productId, quantity });
  };
  
  const clearCart = () => {
    dispatch({ type: 'CLEAR_CART' });
  };
  
  const value = {
    cart,
    addToCart,
    removeFromCart,
    updateQuantity,
    clearCart
  };
  
  return <CartContext.Provider value={value}>{children}</CartContext.Provider>;
};

export const useCart = (): CartContextType => {
  const context = useContext(CartContext);
  if (!context) {
    throw new Error('useCart must be used within a CartProvider');
  }
  return context;
};
