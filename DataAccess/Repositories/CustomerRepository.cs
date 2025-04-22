using System.Data;
using DataAccess.Interfaces;
using Infrastructure.Caching;
using Model.DomainModels;
namespace DataAccess.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        private readonly RedisHelper redis;

        public CustomerRepository(RedisHelper redisHelper) : base()
        {
            redis = redisHelper;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            string customerCacheKey = "AllCustomers";
            var allCustomers = await redis.GetAsync<List<Customer>>(customerCacheKey);

            if (allCustomers != null)
            {
                return allCustomers;
            }

            string query = "SELECT * FROM customers";
            DataTable table = await ExecuteQueryAsync(query);
            List<Customer> customers = new List<Customer>();

            foreach (DataRow row in table.Rows)
            {
                Customer customer = MapToCustomer(row);
                customers.Add(customer);
            }

            await redis.SetAsync(customerCacheKey, customers, TimeSpan.FromMinutes(1));
            return customers;
        }

        public async Task<List<Customer>> GetCustomerByTermAsync(string searchTerm)
        {
            string query = "SELECT * FROM customers WHERE id LIKE @term OR username LIKE @term OR phone LIKE @term OR email LIKE @term";
            var parameters = new Dictionary<string, object?>
            {
                { "term", $"%{searchTerm}%" }
            };

            DataTable table = await ExecuteQueryAsync(query, parameters);
            List<Customer> customers = new List<Customer>();


            foreach (DataRow row in table.Rows)
            {
                Customer customer = MapToCustomer(row);
                customers.Add(customer);
            }

            return customers;
        }

        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "username", customer.UserName },
                { "password", customer.Password },
                { "phone", customer.Phone },
                { "email", customer.Email },
                { "vip_level", customer.VIPlevel },
                { "notes", customer.Notes }
            };

            string query = "INSERT INTO customers (username, password, phone, email, vip_level, notes) VALUES (@username, @password, @phone, @email, @vip_level, @notes)";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "username", customer.UserName },
                { "password", customer.Password },
                { "phone", customer.Phone },
                { "email", customer.Email },
                { "vip_level", customer.VIPlevel },
                { "notes", customer.Notes }
            };

            string query = "UPDATE customers SET username = @username, password = @password, phone = @phone, email = @email, vip_level = @vip_level, notes = @notes WHERE username = @username";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }


        public async Task<bool> DeleteCustomerAsync(string username)
        {
            string query = "DELETE FROM customers WHERE username = @username";
            var parameters = new Dictionary<string, object?>
            {
                { "username", username }
            };
            await ExecuteNonQueryAsync(query, parameters);

            return true;
        }

        public async Task<bool> UpdateNotesAsync(Customer customer)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "username", customer.UserName },
                { "notes", customer.Notes }
            };

            string query = "UPDATE customers SET notes = @notes WHERE username = @username";
            await ExecuteNonQueryAsync(query, parameters);

            return true;
        }

        public async Task<int> GetIDBynameAsync(string username)
        {
            string query = "SELECT id FROM customers WHERE username = @name";
            var parameters = new Dictionary<string, object?>
            {
                { "name", $"%{username}%" }
            };
            DataTable table = await ExecuteQueryAsync(query, parameters);

            if (table.Rows.Count > 0)
                return Convert.ToInt32(table.Rows[0]["id"]);

            return 0;
        }

        private Customer MapToCustomer(DataRow row)
        {
            var customer = new Customer
            {
                ID = Convert.ToInt32(row["id"]),
                UserName = row["username"].ToString(),
                Password = row["password"].ToString(),
                Phone = row["phone"].ToString(),
                Email = row["email"].ToString(),
                VIPlevel = row["vip_level"].ToString(),
                Notes = row["notes"].ToString(),
                CreatedAt = Convert.ToDateTime(row["created_at"])
            };
            return customer;
        }
    }
}