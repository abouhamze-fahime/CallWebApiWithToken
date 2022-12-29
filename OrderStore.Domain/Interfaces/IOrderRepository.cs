using OrderStore.Domain.Models.EshopModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderStore.Domain.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Orders>
    {
       Task<IEnumerable<Orders>> GetOrdersByOrderName(string orderName);
    }
}
