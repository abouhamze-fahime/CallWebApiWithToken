using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderStore.Domain.Interfaces;

namespace OrderStore.Repository
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IStockRepository, StockRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
            return services;
        }
    }
}
