using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStoreManagement.Models
{
    public class Category
    {
        [Key]
        [ForeignKey("Product")]
        public int CategoryID { get; set; }

        public String CategoryName { get; set; }


    }
}
