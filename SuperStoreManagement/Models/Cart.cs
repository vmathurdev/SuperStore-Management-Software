using project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStoreManagement.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        public int Quanity { get; set; }

        public int ProductID { get; set; }

        public int UserID { get; set; }


    }
}
