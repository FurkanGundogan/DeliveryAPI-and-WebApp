using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DeliveryAPI.Models
{
    public partial class Shop
    {
        public Shop()
        {
            Deliveries = new HashSet<Delivery>();
        }

        [Required]
        public int ShopId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
