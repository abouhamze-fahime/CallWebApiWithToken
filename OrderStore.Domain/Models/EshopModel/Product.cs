using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OrderStore.Domain.Models.EshopModel
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        [Key]
        public int ProductId { get; set; }
        [Display(Name ="Product Name")]
        [Required(ErrorMessage ="{0} is required!")]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int StockId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
