using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Infrastructure.Caching;
using Model.DomainModels;

namespace DataAccess.Repositories
{
    public class CustomerOrderRepository : BaseRepository, ICustomerOrderRepository
    {
        private readonly RedisHelper redis;

        public CustomerOrderRepository(RedisHelper redisHelper) : base()
        {
            redis = redisHelper;
        }

        public async Task<List<CustomerOrder>> GetAllCustomerOrdersAsync()
        {
            string custOrdersCacheKey = "AllCustOrds";
            var allOrders = await redis.GetAsync<List<CustomerOrder>>(custOrdersCacheKey);

            if (allOrders != null)
            {
                return allOrders;
            }

            string query = "SELECT * FROM customer_orders";
            DataTable table = await ExecuteQueryAsync(query);
            List<CustomerOrder> orders = new List<CustomerOrder>();
            foreach (DataRow row in table.Rows)
            {
                CustomerOrder order = MapToCustomerOrder(row);
                orders.Add(order);
            }

            await redis.SetAsync(custOrdersCacheKey, orders, TimeSpan.FromMinutes(1));
            return orders;
        }

        public async Task<List<CustomerOrder>> GetCustomerOrdersByCustomerIDAsync(int id)
        {
            string custOrdersCacheKey = $"{id}'s Orders";
            var custOrder = await redis.GetAsync<List<CustomerOrder>>(custOrdersCacheKey);

            if (custOrder != null)
            {
                return custOrder;
            }
            string query = "SELECT * FROM customer_orders WHERE customer_id = @customer_id";
            var parameters = new Dictionary<string, object?>
            {
                { "customer_id", id }
            };

            DataTable table = await ExecuteQueryAsync(query, parameters);
            List<CustomerOrder> orders = new List<CustomerOrder>();
            foreach (DataRow row in table.Rows)
            {
                CustomerOrder order = MapToCustomerOrder(row);
                orders.Add(order);
            }
            await redis.SetAsync(custOrdersCacheKey, orders, TimeSpan.FromMinutes(1));
            return orders;
        }

        public async Task<bool> AddOrderAsync(CustomerOrder order)
        {
            order.ID = Guid.NewGuid();
            var parameters = new Dictionary<string, object?>
            {
                { "id", order.ID },
                { "customer_id", order.CustomerID },
                { "order_date", order.OrderDate },
                { "amount", order.Amount },
                { "gst", order.Gst },
                { "total_amount", order.TotalAmount },
                { "status", order.Status },
                { "operator_id", order.UserID }
            };

            string query = @"INSERT INTO customer_orders 
                (id, customer_id, order_date, amount, gst, total_amount, status, operator_id) 
                VALUES (@id, @customer_id, @order_date, @amount, @gst, @total_amount, @status, @operator_id)";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> UpdateOrderAsync(CustomerOrder order)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "id", order.ID },
                { "customer_id", order.CustomerID },
                { "order_date", order.OrderDate },
                { "amount", order.Amount },
                { "gst", order.Gst },
                { "total_amount", order.TotalAmount },
                { "status", order.Status },
                { "operator_id", order.UserID }
            };

            string query = @"UPDATE customer_orders 
                SET amount = @amount,  gst = @gst, total_amount = @total_amount, status = @status, operator_id = @operator_id 
                WHERE id = @id";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "id", id }
            };

            string query = "DELETE FROM customer_orders WHERE id = @id";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        private CustomerOrder MapToCustomerOrder(DataRow row)
        {
            return new CustomerOrder
            {
                ID = (Guid)row["id"],
                CustomerID = row.Field<int>("customer_id"),
                OrderDate = row.Field<DateTime>("order_date"),
                Amount = row.Field<decimal>("amount"),
                Gst = row.Field<decimal>("gst"),
                TotalAmount = row.Field<decimal>("total_amount"),
                Status = row.Field<string>("status"),
                UserID = row.Field<int>("operator_id"),
                CreatedAt = row.Field<DateTime>("created_at"),
                UpdatedAt = row.Field<DateTime>("updated_at")
            };
        }
    }
}
