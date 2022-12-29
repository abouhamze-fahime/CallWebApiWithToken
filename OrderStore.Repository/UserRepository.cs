using OrderStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderStore.Domain.Models.Users;

namespace OrderStore.Repository
{
   public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext options) :base(options)
        {

        }

        public bool CheckUserExist(UserViewModel user)
        {
            var us =  _context.User.Any(u => u.UserName==user.UserName && u.Password==user.Password);

            if (us)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
