using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class User
    {
        [Key]
        [ForeignKey("Cart")]
        public int UserId { get; set; }

        [Required (ErrorMessage ="Username is required.")]
        [RegularExpression("^[0-9A-Za-z]{5,20}$", ErrorMessage = "Username length should be between 5-20")]
        public String UserName { get; set; }

        [Required(ErrorMessage ="Email is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression("^[0-9A-Za-z]{6,20}$", ErrorMessage = "Enter password length should be between 0-20")]
        public String Password { get; set; }

        [Compare("Password",ErrorMessage = "Please Confirm your password.")]
        [DataType(DataType.Password)]
        public String ConfirmPassword { get; set; }
    }
}
