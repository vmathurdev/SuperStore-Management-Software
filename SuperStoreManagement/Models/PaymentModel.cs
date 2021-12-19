using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperStoreManagement.Models
{
    public class PaymentModel
    {
        [Key]
        public int paymentId { get; set; }

        [Required(ErrorMessage = "Select type of Card")]
        public String cardType { get; set; }

        [Required(ErrorMessage = "Card number is required")]
        public long cardNumber { get; set; }

        [RegularExpression(@"^[a-zA-Z].*[\s\.]*$", ErrorMessage = "Please enter a valid Name")]
        [Required(ErrorMessage = "Card holder name is required")]
        public string cardHolderName { get; set; }

        [NotMapped]
        [RegularExpression(@"^(0[1-9]|10|11|12)\/20(1[6-9]|2[0-9]|3[0-1])$", ErrorMessage = "Please enter a valid expiry Date between 2021-2031(MM//YYYY). ")]
        [Required(ErrorMessage = "Expiry date is required")]
        public string expiryDate { get; set; }

        [NotMapped]
        [RegularExpression(@"^[0-9]{3,4}$", ErrorMessage = "Please enter a valid CVV")]
        [Required(ErrorMessage = "CVV is required")]
        public string cvv { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Country is required")]
        public string country { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Post code is required")]
        public string postalCode { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Province/State should not contain any special characters or numbers")]
        [StringLength(20, ErrorMessage = "Province/State should not contain more than 20 characters")]
        public string province { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "City should not contain any special characters or numbers")]
        [StringLength(20, ErrorMessage = "City should not contain more than 20 characters")]
        public string city { get; set; }
    }


}
