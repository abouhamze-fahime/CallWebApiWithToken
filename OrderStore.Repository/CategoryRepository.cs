using OrderStore.Domain.Interfaces;
using OrderStore.Domain.Models.EshopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderStore.Repository
{
   public class CategoryRepository :GenericRepository<Categories> ,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext option):base(option)
        {

        }
    }
}
