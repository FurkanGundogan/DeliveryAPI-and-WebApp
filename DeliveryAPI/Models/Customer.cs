using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DeliveryAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Deliveries = new HashSet<Delivery>();
        }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int PhoneNumber { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
