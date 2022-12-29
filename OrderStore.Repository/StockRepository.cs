using OrderStore.Domain.Interfaces;
using OrderStore.Domain.Models.EshopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderStore.Repository
{
  public  class StockRepository :GenericRepository<Stock> ,IStockRepository
    {
        public StockRepository(ApplicationDbContext option):base(option)
        {

        }
    }
}
