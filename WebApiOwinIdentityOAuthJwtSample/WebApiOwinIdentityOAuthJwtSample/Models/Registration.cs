using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiOwinIdentityOAuthJwtSample.Models
{
    public class Registration
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(Int32.MaxValue, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirmation{ get; set; }
    }
}