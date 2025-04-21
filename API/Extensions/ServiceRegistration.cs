using Business.Interfaces;
using Business.Users;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Service.Implementations;
using Optional.Caching;
using Business.Customers;
using Service.Interfaces;
using Business.Accounts;
using Business.Suppliers;
using Business.Roles;
using Business.Addresses;
using Service.Shared;
using Business.Products;
using Business.HelpDocs;
using Business.Events;
using DataAccess.EF;
using Business.ProductCostChanges;
using Business.ProductPriceChanges;
using Business.ProductStockChanges;
using Business.ProductSuppliers;
using Business.CustomerOrders;
using Business.CustomerOrderDetails;
using Business.SupplierOrders;
using Business.SupplierOrderDetails;
using Business.StockEntries;

namespace API.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            // Add Redis
            services.AddScoped<RedisHelper>();
            // Add Transaction
            services.AddScoped<TransactionExecutor>();
            // Add DataAccess
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IHelpDocRepository, HelpDocRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IProductCostChangeRepository, ProductCostChangeRepository>();
            services.AddScoped<IProductPriceChangeRepository, ProductPriceChangeRepository>();
            services.AddScoped<IProductStockChangeRepository, ProductStockChangeRepository>();
            services.AddScoped<IProductSupplierRepository, ProductSupplierRepository>();
            services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
            services.AddScoped<ICustomerOrderDetailRepository, CustomerOrderDetailRepository>();
            services.AddScoped<ISupplierOrderRepository, SupplierOrderRepository>();
            services.AddScoped<ISupplierOrderDetailRepository, SupplierOrderDetailRepository>();
            services.AddScoped<IStockEntryRepository, StockEntryRepository>();
            // Add Business
            services.AddScoped<ICustomerBusiness, CustomerBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IAccountBusiness, AccountBusiness>();
            services.AddScoped<ISupplierBusiness, SupplierBusiness>();
            services.AddScoped<IRoleBusiness, RoleBusiness>();
            services.AddScoped<IAddressBusiness, AddressBusiness>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddScoped<IHelpDocBusiness, HelpDocBusiness>();
            services.AddScoped<IEventBusiness, EventBusiness>();
            services.AddScoped<IProductCostChangeBusiness, ProductCostChangeBusiness>();
            services.AddScoped<IProductPriceChangeBusiness, ProductPriceChangeBusiness>();
            services.AddScoped<IProductStockChangeBusiness, ProductStockChangeBusiness>();
            services.AddScoped<IProductSupplierBusiness, ProductSupplierBusiness>();
            services.AddScoped<ICustomerOrderBusiness, CustomerOrderBusiness>();
            services.AddScoped<ICustomerOrderDetailBusiness, CustomerOrderDetailBusiness>();
            services.AddScoped<ISupplierOrderBusiness, SupplierOrderBusiness>();
            services.AddScoped<ISupplierOrderDetailBusiness, SupplierOrderDetailBusiness>();
            services.AddScoped<IStockEntryBusiness, StockEntryBusiness>();
            // Add Service
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IHelpDocService, HelpDocService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IProductCostChangeService, ProductCostChangeService>();
            services.AddScoped<IProductPriceChangeService, ProductPriceChangeService>();
            services.AddScoped<IProductStockChangeService, ProductStockChangeService>();
            services.AddScoped<IProductSupplierService, ProductSupplierService>();
            services.AddScoped<ICustomerOrderService, CustomerOrderService>();
            services.AddScoped<ICustomerOrderDetailService, CustomerOrderDetailService>();
            services.AddScoped<ISupplierOrderService, SupplierOrderService>();
            services.AddScoped<ISupplierOrderDetailService, SupplierOrderDetailService>();
            services.AddScoped<IStockEntryService, StockEntryService>();
        }
    }
}