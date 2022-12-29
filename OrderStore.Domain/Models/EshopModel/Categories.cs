using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OrderStore.Domain.Models.EshopModel
{
    public partial class Categories
    {
        public Categories()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        public int CategoryId { get; set; }
        [Display(Name ="Product Category  Name")]
        [Required(ErrorMessage ="{0} is required")]
        public string Name { get; set; }
        [StringLength(150)]
        public string Description { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
