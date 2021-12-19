using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperStoreManagement.Models
{
    public class Product
    {
        [Key]
        [ForeignKey("Cart")]
        public int prodID { get; set; }

        public String Name { get; set; }

        public String  Description { get; set; }

        public float  Price { get; set; }

        public String image { get; set; }

        public int stock { get; set; }

        public int category { get; set; }

        public virtual Category Category { get; set; }


    }
}
