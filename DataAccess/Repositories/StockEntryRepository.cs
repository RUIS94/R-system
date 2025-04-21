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
    public class StockEntryRepository : BaseRepository, IStockEntryRepository
    {
        public async Task<List<StockEntry>> GetAllStockEntriesAsync()
        {
            string query = "SELECT * FROM stock_entries";
            DataTable table = await ExecuteQueryAsync(query);
            List<StockEntry> entries = new List<StockEntry>();

            foreach (DataRow row in table.Rows)
            {
                StockEntry entry = MapToStockEntry(row);
                entries.Add(entry);
            }

            return entries;
        }

        public async Task<List<StockEntry>> GetStockEntriesBySupplierOrderIDAsync(Guid supplierOrderId)
        {
            string query = "SELECT * FROM stock_entries WHERE supplier_order_id = @supplier_order_id";
            var parameters = new Dictionary<string, object?>
            {
                { "supplier_order_id", supplierOrderId }
            };

            DataTable table = await ExecuteQueryAsync(query, parameters);
            List<StockEntry> entries = new List<StockEntry>();

            foreach (DataRow row in table.Rows)
            {
                StockEntry entry = MapToStockEntry(row);
                entries.Add(entry);
            }

            return entries;
        }

        public async Task<bool> AddStockEntryAsync(StockEntry entry)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "product_id", entry.ProductID },
                { "supplier_order_id", entry.SupplierOrderID },
                { "quantity", entry.Quantity },
                { "cost", entry.Cost },
                { "gst", entry.Gst },
                { "total_amount", entry.TotalAmount },
                { "entry_date", entry.EntryDate },
                { "updated_at", entry.UpdatedAt }
            };

            string query = @"INSERT INTO stock_entries 
                (product_id, supplier_order_id, quantity, cost, gst, total_amount, entry_date, updated_at) 
                VALUES (@product_id, @supplier_order_id, @quantity, @cost, @gst, @total_amount, @entry_date, @updated_at)";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> UpdateStockEntryAsync(StockEntry entry)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "id", entry.ID },
                { "product_id", entry.ProductID },
                { "supplier_order_id", entry.SupplierOrderID },
                { "quantity", entry.Quantity },
                { "cost", entry.Cost },
                { "gst", entry.Gst },
                { "total_amount", entry.TotalAmount },
                { "updated_at", entry.UpdatedAt }
            };

            string query = @"UPDATE stock_entries 
                SET product_id = @product_id, supplier_order_id = @supplier_order_id, quantity = @quantity, 
                    cost = @cost, gst = @gst, total_amount = @total_amount, updated_at = @updated_at 
                WHERE id = @id";

            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> DeleteStockEntryAsync(int id)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "id", id }
            };

            string query = "DELETE FROM stock_entries WHERE id = @id";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        private StockEntry MapToStockEntry(DataRow row)
        {
            return new StockEntry
            {
                ID = (int)row["id"],
                ProductID = (int)row["product_id"],
                SupplierOrderID = (Guid)row["supplier_order_id"],
                Quantity = (int)row["quantity"],
                Cost = (decimal)row["cost"],
                Gst = (decimal)row["gst"],
                TotalAmount = (decimal)row["total_amount"],
                EntryDate = (DateTime)row["entry_date"],
                UpdatedAt = (DateTime)row["updated_at"]
            };
        }
    }
}