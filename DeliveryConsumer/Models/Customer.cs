using System;
using System.Collections.Generic;

#nullable disable

namespace DeliveryConsumer.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Deliveries = new HashSet<Delivery>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
