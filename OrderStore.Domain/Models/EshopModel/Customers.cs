using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OrderStore.Domain.Models.EshopModel
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        public int CustomerId { get; set; }
        [Display(Name ="Customer Name")]
        [Required(ErrorMessage ="{0} is required")]
        [StringLength(150)]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(10)]
        public string Nationalcode { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(50)]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(150)]
        public string Address { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
