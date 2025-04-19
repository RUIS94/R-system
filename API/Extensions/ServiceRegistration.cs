using Business.Interfaces;
using Business.Users;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Service.Implementations;
using Optional.Caching;
using Microsoft.Extensions.DependencyInjection;
using Business.Customers;
using Service.Interfaces;

namespace API.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            // 添加 Redis 配置
            services.AddScoped<RedisHelper>();

            // 添加数据访问层服务
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // 添加业务规则层服务
            services.AddScoped<ICustomerBusinessRules, CustomerBusinessRules>();
            services.AddScoped<IUserBusinessRules, UserBusinessRules>();

            // 添加服务
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}