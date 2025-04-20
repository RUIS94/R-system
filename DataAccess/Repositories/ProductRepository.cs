using System.Data;
using DataAccess.Interfaces;
using Model.DomainModels;
using Optional.Caching;

namespace DataAccess.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly RedisHelper redis;

        public ProductRepository(RedisHelper redisHelper) : base()
        {
            redis = redisHelper;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            string productCacheKey = "AllProducts";
            var allProducts = await redis.GetAsync<List<Product>>(productCacheKey);

            if (allProducts != null)
            {
                return allProducts;
            }
            string query = "SELECT * FROM products";
            DataTable dataTable = await ExecuteQueryAsync(query);

            List<Product> products = new List<Product>();

            foreach (DataRow row in dataTable.Rows)
            {
                Product product = MapToProduct(row);
                products.Add(product);
            }
            await redis.SetAsync(productCacheKey, products, TimeSpan.FromMinutes(1));
            return products;
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            string productCacheKey = $"{category}Prod";
            var catProds = await redis.GetAsync<List<Product>>(productCacheKey);

            if (catProds != null)
            {
                return catProds;
            }
            string query = "SELECT * FROM products WHERE category = @category";
            var parameters = new Dictionary<string, object?>
            {
                { "category", category }
            };
            DataTable table = await ExecuteQueryAsync(query, parameters);
            List<Product> products = new List<Product>();
            foreach (DataRow row in table.Rows)
            {
                Product product = MapToProduct(row);
                products.Add(product);
            }
            await redis.SetAsync(productCacheKey, products, TimeSpan.FromMinutes(1));
            return products;
        }

        public async Task<Product?> GetProductByBarcodeAsync(string barcode)
        {
            string productCacheKey = $"{barcode}Prod";
            var barProd = await redis.GetAsync<Product>(productCacheKey);

            if (barProd != null)
            {
                return barProd;
            }
            string query = "SELECT * FROM products WHERE barcode = @barcode";
            var parameters = new Dictionary<string, object?>
            {
                { "barcode", barcode }
            };
            DataTable table = await ExecuteQueryAsync(query, parameters);

            if (table.Rows.Count > 0)
            {
                Product product = MapToProduct(table.Rows[0]);
                await redis.SetAsync(productCacheKey, product, TimeSpan.FromMinutes(1));
                return product;
            }

            return null;
        }

        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            string query = "DELETE FROM products WHERE id = @id";
            var parameters = new Dictionary<string, object?>
            {
                { "id", id }
            };

            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "name", product.Name },
                { "barcode", product.Barcode },
                { "cost", product.Cost },
                { "price", product.Price },
                { "inventory", product.Inventory },
                { "description", product.Description },
                { "category", product.Category },
                { "gst", product.Gst }
            };
            string query = @"INSERT INTO customers 
                (name, barcode, cost, price, inventory, description, category, gst) 
                VALUES (@name, @barcode, @cost, @price, @inventory, @description, @category, @gst)";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }
        
        public async Task<bool> UpdateProductAsync(Product product)
        {
            string query = @"
                UPDATE products SET
                    name = @name,
                    barcode = @barcode,
                    cost = @cost,
                    price = @price,
                    inventory = @inventory,
                    description = @description,
                    category = @category,
                    gst = @gst
                WHERE id = @id";

            var parameters = new Dictionary<string, object?>
            {
                { "id", product.ID },
                { "name", product.Name },
                { "barcode", product.Barcode },
                { "cost", product.Cost },
                { "price", product.Price },
                { "inventory", product.Inventory },
                { "description", product.Description },
                { "category", product.Category },
                { "gst", product.Gst }
            };

            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> UpdateInventoryAsync(int id, int inventory)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "id", id },
                { "inventory", inventory }
            };
            string query = "UPDATE products SET inventory = @inventory WHERE id = @id";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<List<Product>> GetProductBynameAsync(string name)
        {
            string query = "SELECT * FROM products WHERE name LIKE @name";
            var parameters = new Dictionary<string, object?>
            {
                { "name", $"%{name}%" }
            };
            DataTable table = await ExecuteQueryAsync(query, parameters);
            List<Product> products = new List<Product>();
            foreach (DataRow row in table.Rows)
            {
                Product product = MapToProduct(row);
                products.Add(product);
            }
            return products;
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            string query = "SELECT COUNT(1) FROM products WHERE id = @id";
            var parameters = new Dictionary<string, object?> { { "id", id } };
            string? result = await ExecuteScalarAsync(query, parameters);
            return result != null && int.TryParse(result, out int count) && count > 0;
        }

        private Product MapToProduct(DataRow row)
        {
            return new Product
            {
                ID = Convert.ToInt32(row["id"]),
                Name = row["name"] as string,
                Barcode = row["barcode"] as string,
                Cost = Convert.ToDecimal(row["cost"]),
                Price = Convert.ToDecimal(row["price"]),
                Inventory = Convert.ToInt32(row["inventory"]),
                Description = row["description"] as string,
                Category = row["category"] as string,
                CreatedAt = Convert.ToDateTime(row["created_at"]),
                UpdatedAt = Convert.ToDateTime(row["updated_at"]),
                Gst = Convert.ToDecimal(row["gst"])
            };
        }
    }
}