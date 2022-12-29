using System;
using System.Collections.Generic;

#nullable disable

namespace OrderStore.Domain.Models.EshopModel
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public decimal TotoalPrice { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
