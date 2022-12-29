using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OrderStore.Domain.Models.EshopModel
{
    public partial class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        [Required(ErrorMessage ="{0} is required")]
        public int Quantity { get; set; }
        public int StockId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public decimal RowSum { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
