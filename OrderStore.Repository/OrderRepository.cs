using OrderStore.Domain.Interfaces;
using OrderStore.Domain.Models.EshopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderStore.Repository
{
   public class OrderRepository: GenericRepository<Orders>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context): base(context)
        {

        }
        public virtual async Task<IEnumerable<Orders>> GetOrdersByOrderName(string customerName)
        {
             return await _context.Orders.Where(c=> c.Customer.Name.Contains(customerName)).ToListAsync();
        }
    }
}
