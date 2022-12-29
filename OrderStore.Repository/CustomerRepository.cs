using OrderStore.Domain.Interfaces;
using OrderStore.Domain.Models.EshopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderStore.Repository
{
   public  class CustomerRepository :GenericRepository<Customers> ,ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext option):base(option)
        {

        }

        public virtual int CustomerCount()
        {
            return  _context.Customers.Count();
        }
    }
}
