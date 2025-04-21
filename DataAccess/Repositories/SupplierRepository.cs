using System.Data;
using DataAccess.Interfaces;
using Infrastructure.Caching;
using Model.DomainModels;

namespace DataAccess.Repositories
{
    public class SupplierRepository : BaseRepository, ISupplierRepository
    {
        private readonly RedisHelper redis;

        public SupplierRepository(RedisHelper redisHelper) : base()
        {
            redis = redisHelper;
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            string supplierCacheKey = "AllSuppliers";
            var allSuppliers = await redis.GetAsync<List<Supplier>>(supplierCacheKey);

            if (allSuppliers != null)
            {
                return allSuppliers;
            }

            string query = "SELECT * FROM suppliers";
            DataTable table = await ExecuteQueryAsync(query);
            List<Supplier> suppliers = new List<Supplier>();

            foreach (DataRow row in table.Rows)
            {
                Supplier supplier = MapToSupplier(row);
                suppliers.Add(supplier);
            }

            await redis.SetAsync(supplierCacheKey, suppliers, TimeSpan.FromMinutes(1));
            return suppliers;
        }

        public async Task<List<Supplier>> GetSupplierByTermAsync(string searchTerm)
        {
            string query = "SELECT * FROM supplier WHERE id LIKE @term OR name LIKE @term OR phone LIKE @term OR email LIKE @term";
            var parameters = new Dictionary<string, object?>
            {
                { "term", $"%{searchTerm}%" }
            };

            DataTable table = await ExecuteQueryAsync(query, parameters);
            List<Supplier> suppliers = new List<Supplier>();


            foreach (DataRow row in table.Rows)
            {
                Supplier supplier = MapToSupplier(row);
                suppliers.Add(supplier);
            }

            return suppliers;
        }

        public async Task<bool> AddSupplierAsync(Supplier supplier)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "name", supplier.Name },
                { "contact_name", supplier.ContactName },
                { "phone", supplier.Phone },
                { "email", supplier.Email },
                { "street_address", supplier.StreetAddress },
                { "city", supplier.City },
                { "state", supplier.State },
                { "country", supplier.Country },
                { "zip_code", supplier.ZipCode }
            };
            string query = @"INSERT INTO suppliers (name, contact_name, phone, email, 
                street_address, city, state, country, zip_code) 
                VALUES (@name, @contact_name, @phone, @email, 
                @street_address, @city, @state, @country, @zip_code)";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> UpdateSupplierAsync(Supplier supplier)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "id", supplier.ID },
                { "name", supplier.Name },
                { "contact_name", supplier.ContactName },
                { "phone", supplier.Phone },
                { "email", supplier.Email },
                { "street_address", supplier.StreetAddress },
                { "city", supplier.City },
                { "state", supplier.State },
                { "country", supplier.Country },
                { "zip_code", supplier.ZipCode }
            };

            string query = @"UPDATE suppliers 
                SET name = @name, contact_name = @contact_name, phone = @phone, email = @email, 
                street_address = @street_address, city = @city state = @state, country = @country, zip_code = @zip_code
                WHERE id = @id";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }


        public async Task<bool> DeleteSupplierAsync(int id)
        {
            string query = "DELETE FROM suppliers WHERE id = @id";
            var parameters = new Dictionary<string, object?>
            {
                { "id", id }
            };
            await ExecuteNonQueryAsync(query, parameters);

            return true;
        }

        public async Task<bool> SupplierExistsAsync(int id)
        {
            string query = "SELECT COUNT(1) FROM suppliers WHERE id = @id";
            var parameters = new Dictionary<string, object?> { { "id", id } };
            string? result = await ExecuteScalarAsync(query, parameters);
            return result != null && int.TryParse(result, out int count) && count > 0;
        }

        private Supplier MapToSupplier(DataRow row)
        {
            var supplier = new Supplier
            {
                ID = Convert.ToInt32(row["id"]),
                Name = row["name"].ToString(),
                ContactName = row["contact_name"].ToString(),
                Phone = row["phone"].ToString(),
                Email = row["email"].ToString(),
                StreetAddress = row["street_address"].ToString(),
                City = row["city"].ToString(),
                State = row["state"].ToString(),
                Country = row["country"].ToString(),
                ZipCode = row["zip_code"].ToString(),
                CreatedAt = Convert.ToDateTime(row["created_at"]),
                UpdatedAt = Convert.ToDateTime(row["updated_at"])
            };
            return supplier;
        }
    }
}