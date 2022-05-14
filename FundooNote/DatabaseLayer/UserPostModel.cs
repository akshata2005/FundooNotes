using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer
{
     public class UserPostModel
    {
        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "First name is not valid")]
        [DataType(DataType.Text)]
        public string firstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Last name is not valid")]
        [DataType(DataType.Text)]
        public string lastName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{3,}([.][A-Za-z0-9]{3,})?[@][A-Za-z]{2,}[.][A-Za-z]{2,}([.][a-zA-Z]{2})?$", ErrorMessage = "EmailId is not valid")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        public DateTime registerdDate { get; set; }

        [Required]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,15})$", ErrorMessage = "Password is not valid")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s,.]+$", ErrorMessage = "Address is not valid")]
        [DataType(DataType.Text)]
        public string address { get; set; }
    }
}
