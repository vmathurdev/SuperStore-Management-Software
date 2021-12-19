using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStoreManagement.Models
{
    public class Order
    {
        [Key]
        public int orderId { get; set; }
        public int UserId { get; set; }

        public float totalPrice { get; set; }

        public DateTime date { get; set; }

    }
}
