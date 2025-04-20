using Business.Interfaces;
using Business.Users;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Service.Implementations;
using Optional.Caching;
using Microsoft.Extensions.DependencyInjection;
using Business.Customers;
using Service.Interfaces;
using Business.Accounts;

namespace API.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            // Add Redis
            services.AddScoped<RedisHelper>();

            // Add DataAccess
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Add Business
            services.AddScoped<ICustomerBusiness, CustomerBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IAccountBusiness, AccountBusiness>();

            // Add Service
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}