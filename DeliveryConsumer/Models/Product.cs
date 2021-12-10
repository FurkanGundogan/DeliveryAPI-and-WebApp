using System;
using System.Collections.Generic;

#nullable disable

namespace DeliveryConsumer.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public int? DeliveryId { get; set; }

        public virtual Delivery Delivery { get; set; }
    }
}
