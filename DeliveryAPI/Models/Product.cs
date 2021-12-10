using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DeliveryAPI.Models
{
    public partial class Product
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? DeliveryId { get; set; }

        public virtual Delivery Delivery { get; set; }
    }
}
