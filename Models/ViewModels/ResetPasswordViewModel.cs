using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        //[Required]
        //public string? Email { get; set; }
        //[Required]
        //public string? Token { get; set; }
        //[Required, DataType(DataType.Password)]
        //public string? Password { get; set; }
        //[Required, DataType(DataType.Password)]
        //[Compare("NewPassword")]
        //public string? ConfirmNewPassword { get; set; }
        //public bool IsSuccess { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
    }
}
