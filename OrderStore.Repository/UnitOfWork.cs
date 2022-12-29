using OrderStore.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace OrderStore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IOrderRepository Orders { get; }
        public IProductRepository Products { get; }
        public ICategoryRepository Category { get; }
        public ICustomerRepository Customer { get; }
        public IOrderDetailRepository OrderDetail { get; }
        public IStockRepository Stock { get; }
        public IUserRepository User { get; }
        public IRoleRepository Role { get; }
        public IUserRoleRepository UserRole { get; }



        public UnitOfWork(ApplicationDbContext context,
            IOrderRepository booksRepository,
            IProductRepository catalogueRepository,
            IStockRepository stockRepository,
            IOrderDetailRepository orderDetailRepository,
            ICustomerRepository customerRepository ,
            ICategoryRepository categoryRepository ,
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository
            )
        {
            this._context = context;
            this.Orders = booksRepository;
            this.Products = catalogueRepository;
            this.OrderDetail = orderDetailRepository;
            this.Customer = customerRepository;
            this.Stock = stockRepository;
            this.Category = categoryRepository;
            this.User = userRepository;
            this.Role = roleRepository;
            this.UserRole = userRoleRepository;
        }
        public async Task<int>  Save()
        {
            return  await _context.SaveChangesAsync();
        }
        public void Dispose()
        {

            // _context.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
