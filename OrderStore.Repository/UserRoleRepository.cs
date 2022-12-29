using OrderStore.Domain.Interfaces;
using OrderStore.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderStore.Repository
{
  public  class UserRoleRepository :GenericRepository<UserRole> , IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext options) :base(options)
        {

        }
    }
}
