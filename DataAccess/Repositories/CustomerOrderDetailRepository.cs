using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace DataAccess.Repositories
{
    public class CustomerOrderDetailRepository : BaseRepository, ICustomerOrderDetailRepository
    {
        public async Task<List<CustomerOrderDetail>> GetAllOrderDetailsAsync()
        {
            string query = "SELECT * FROM customer_order_details";
            DataTable table = await ExecuteQueryAsync(query);
            List<CustomerOrderDetail> details = new List<CustomerOrderDetail>();

            foreach (DataRow row in table.Rows)
            {
                CustomerOrderDetail detail = MapToCustomerOrderDetail(row);
                details.Add(detail);
            }

            return details;
        }

        public async Task<List<CustomerOrderDetail>> GetOrderDetailsByOrderIDAsync(Guid orderId)
        {
            string query = "SELECT * FROM customer_order_details WHERE order_id = @order_id";
            var parameters = new Dictionary<string, object?>
            {
                { "order_id", orderId }
            };

            DataTable table = await ExecuteQueryAsync(query, parameters);
            List<CustomerOrderDetail> details = new List<CustomerOrderDetail>();

            foreach (DataRow row in table.Rows)
            {
                CustomerOrderDetail detail = MapToCustomerOrderDetail(row);
                details.Add(detail);
            }

            return details;
        }

        public async Task<bool> AddOrderDetailAsync(CustomerOrderDetail detail)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "order_id", detail.OrderID },
                { "product_id", detail.ProductID },
                { "quantity", detail.Quantity },
                { "unit_price", detail.UnitPrice },
                { "discount", detail.Discount },
                { "total_price", detail.TotalPrice },
                { "gst", detail.Gst }
            };

            string query = @"INSERT INTO customer_order_details 
                (order_id, product_id, quantity, unit_price, discount, total_price, gst) 
                VALUES (@order_id, @product_id, @quantity, @unit_price, @discount, @total_price, @gst)";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> UpdateOrderDetailAsync(CustomerOrderDetail detail)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "id", detail.ID },
                { "order_id", detail.OrderID },
                { "product_id", detail.ProductID },
                { "quantity", detail.Quantity },
                { "unit_price", detail.UnitPrice },
                { "discount", detail.Discount },
                { "total_price", detail.TotalPrice },
                { "gst", detail.Gst }
            };

            string query = @"UPDATE customer_order_details 
                SET product_id = @product_id, quantity = @quantity, unit_price = @unit_price, 
                    discount = @discount, total_price = @total_price, gst = @gst 
                WHERE id = @id";

            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> DeleteOrderDetailAsync(int id)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "id", id }
            };

            string query = "DELETE FROM customer_order_details WHERE id = @id";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        private CustomerOrderDetail MapToCustomerOrderDetail(DataRow row)
        {
            return new CustomerOrderDetail
            {
                ID = (int)row["id"],
                OrderID = (Guid)row["order_id"],
                ProductID = (int)row["product_id"],
                Quantity = (int)row["quantity"],
                UnitPrice = (decimal)row["unit_price"],
                Discount = (decimal)row["discount"],
                TotalPrice = (decimal)row["total_price"],
                CreatedAt = (DateTime)row["created_at"],
                UpdatedAt = (DateTime)row["updated_at"],
                Gst = (decimal)row["gst"]
            };
        }
    }
}
