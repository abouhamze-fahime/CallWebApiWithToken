﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderStore.Domain.Models.Users;

namespace OrderStore.Domain.Interfaces
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
    }
}
