using System;
using System.Collections.Generic;

#nullable disable

namespace DeliveryConsumer.Models
{
    public partial class Delivery
    {
        public Delivery()
        {
            Products = new HashSet<Product>();
        }

        public int DeliveryId { get; set; }
        public int? CustomerId { get; set; }
        public int? ShopId { get; set; }
        public DateTime ArriveDate { get; set; }
        public string Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
