using System;
using OrderStore.Domain.Models.EshopModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderStore.Domain.Interfaces
{
    public interface ICustomerRepository :IGenericRepository<Customers>
    {
        int CustomerCount();
    }
}
