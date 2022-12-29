using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OrderStore.Domain.Models.Users
{
    public partial class User
    {
        public User()
        {
            UserRole = new HashSet<UserRole>();
        }

        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
