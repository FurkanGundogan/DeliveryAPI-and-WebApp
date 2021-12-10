using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DeliveryAPI.Models
{
    public partial class Delivery
    {
        public Delivery()
        {
            Products = new HashSet<Product>();
        }
        [Required]
        public int DeliveryId { get; set; }
        [Required]
        public int? CustomerId { get; set; }
        [Required]
        public int? ShopId { get; set; }
        [Required]
        public DateTime ArriveDate { get; set; }
        [Required]
        public string Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
