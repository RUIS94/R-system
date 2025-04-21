
import { Button } from "@/components/ui/button";

interface CategoryFilterProps {
  categories: string[];
  selectedCategory: string | null;
  onSelectCategory: (category: string | null) => void;
}

export function CategoryFilter({ 
  categories, 
  selectedCategory, 
  onSelectCategory 
}: CategoryFilterProps) {
  return (
    <div className="flex flex-wrap gap-2 mb-6">
      <Button
        variant={selectedCategory === null ? "default" : "outline"}
        onClick={() => onSelectCategory(null)}
        className={selectedCategory === null ? "bg-blue-600 hover:bg-blue-700" : ""}
      >
        All
      </Button>
      
      {categories.map(category => (
        <Button
          key={category}
          variant={selectedCategory === category ? "default" : "outline"}
          onClick={() => onSelectCategory(category)}
          className={selectedCategory === category ? "bg-blue-600 hover:bg-blue-700" : ""}
        >
          {category}
        </Button>
      ))}
    </div>
  );
}
