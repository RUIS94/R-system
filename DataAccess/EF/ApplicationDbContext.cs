using Microsoft.EntityFrameworkCore;
using Model.DomainModels;

namespace DataAccess.EF
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext()
        {
            _connectionString = new DbConnection().ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                _connectionString,
                ServerVersion.AutoDetect(_connectionString)
            );
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerOrderDetail> CustomerOrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCostChange> ProductCostChanges { get; set; }
        public DbSet<ProductPriceChange> ProductPriceChanges { get; set; }
        public DbSet<ProductStockChange> ProductStockChanges { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierOrder> SupplierOrders { get; set; }
        public DbSet<SupplierOrderDetail> SupplierOrderDetails { get; set; }
        public DbSet<StockEntry> StockEntries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<HelpDoc> HelpDocs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
