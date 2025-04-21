using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Model.DomainModels;
using Infrastructure.Caching;

namespace DataAccess.Repositories
{
    public class SupplierOrderRepository : BaseRepository, ISupplierOrderRepository
    {
        private readonly RedisHelper redis;

        public SupplierOrderRepository(RedisHelper redisHelper) : base()
        {
            redis = redisHelper;
        }

        public async Task<List<SupplierOrder>> GetAllSupplierOrdersAsync()
        {
            string suppOrdersCacheKey = "AllSuppOrds";
            var allOrders = await redis.GetAsync<List<SupplierOrder>>(suppOrdersCacheKey);

            if (allOrders != null)
            {
                return allOrders;
            }

            string query = "SELECT * FROM supplier_orders";
            DataTable table = await ExecuteQueryAsync(query);
            List<SupplierOrder> orders = new List<SupplierOrder>();
            foreach (DataRow row in table.Rows)
            {
                SupplierOrder order = MapToSupplierOrder(row);
                orders.Add(order);
            }

            await redis.SetAsync(suppOrdersCacheKey, orders, TimeSpan.FromMinutes(1));
            return orders; 
        }

        public async Task<List<SupplierOrder>> GetSupplierOrdersBySupplierIDAsync(int id)
        {
            string suppOrdersCacheKey = $"(Supp){id}'s Orders";
            var suppOrder = await redis.GetAsync<List<SupplierOrder>>(suppOrdersCacheKey);

            if (suppOrder != null)
            {
                return suppOrder;
            }
            string query = "SELECT * FROM supplier_orders WHERE supplier_id = @supplier_id";
            var parameters = new Dictionary<string, object?>
            {
                { "supplier_id", id }
            };

            DataTable table = await ExecuteQueryAsync(query, parameters);
            List<SupplierOrder> orders = new List<SupplierOrder>();
            foreach (DataRow row in table.Rows)
            {
                SupplierOrder order = MapToSupplierOrder(row);
                orders.Add(order);
            }
            await redis.SetAsync(suppOrdersCacheKey, orders, TimeSpan.FromMinutes(1));
            return orders;
        }


        public async Task<bool> AddOrderAsync(SupplierOrder order)
        {
            order.ID = Guid.NewGuid();
            var parameters = new Dictionary<string, object?>
            {
                { "id", order.ID },
                { "supplier_id", order.SupplierID },
                { "order_date", order.OrderDate },
                { "gst", order.Gst },
                { "total_amount", order.TotalAmount },
                { "status", order.Status }
            };

            string query = @"INSERT INTO supplier_orders 
                (id, supplier_id, order_date, gst, total_amount, status) 
                VALUES (@id, @supplier_id, @order_date, @gst, @total_amount, @status)";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> UpdateOrderAsync(SupplierOrder order)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "id", order.ID },
                { "supplier_id", order.SupplierID },
                { "order_date", order.OrderDate },
                { "gst", order.Gst },
                { "total_amount", order.TotalAmount },
                { "status", order.Status }
            };

            string query = @"UPDATE supplier_orders 
                SET gst = @gst, total_amount = @total_amount, status = @status 
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

            string query = "DELETE FROM supplier_orders WHERE id = @id";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        private SupplierOrder MapToSupplierOrder(DataRow row)
        {
            return new SupplierOrder
            {
                ID = (Guid)row["id"],
                SupplierID = row.Field<int>("customer_id"),
                OrderDate = row.Field<DateTime>("order_date"),
                Gst = row.Field<decimal>("gst"),
                TotalAmount = row.Field<decimal>("total_amount"),
                Status = row.Field<string>("status"),
                CreatedAt = row.Field<DateTime>("created_at"),
                UpdatedAt = row.Field<DateTime>("updated_at")
            };
        }
    }
}
