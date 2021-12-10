using System;
using System.Collections.Generic;

#nullable disable

namespace DeliveryConsumer.Models
{
    public partial class Shop
    {
        public Shop()
        {
            Deliveries = new HashSet<Delivery>();
        }

        public int ShopId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
