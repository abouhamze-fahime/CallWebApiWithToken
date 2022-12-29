using System;
using System.Threading.Tasks;

namespace OrderStore.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        IOrderDetailRepository OrderDetail { get; }
        ICustomerRepository Customer { get; }
        IStockRepository Stock { get; }
        ICategoryRepository Category { get; }
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IUserRoleRepository UserRole { get; }
        Task<int> Save();
    }
}
