using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Pleace Enter your Email")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Pleace Enter a valid Eamil")]
        public string? Email { get; set; }
        public bool EmailSent { get; set; }

    }
}
