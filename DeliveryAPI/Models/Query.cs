using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAPI.Models
{
    public class Query
    {
        [Required]
        public string status { get; set; }
    }
}
