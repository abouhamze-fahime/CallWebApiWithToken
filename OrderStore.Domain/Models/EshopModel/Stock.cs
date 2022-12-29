using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OrderStore.Domain.Models.EshopModel
{
    public partial class Stock
    {
        public Stock()
        {
            OrderDetails = new HashSet<OrderDetails>();
            Product = new HashSet<Product>();
        }
        [Key]
        public int StockId { get; set; }
        [Required(ErrorMessage ="{0} is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage ="{0} is required")]
        public int QuantityInStock { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
