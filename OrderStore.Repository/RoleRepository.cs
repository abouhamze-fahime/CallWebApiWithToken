using OrderStore.Domain.Interfaces;
using OrderStore.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrderStore.Repository 
{
 public   class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext options) :base(options)
        {

        }
    }
}
