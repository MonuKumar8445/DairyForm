using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CreateViewModel
    {
        public string? Id { get; set; }
       
        [Required(ErrorMessage = "Pleace Enter your FirstName")]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "FirstName")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Pleace Enter your LastName")]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "LastName")]
        public string? LastName { get; set; }
        [EmailAddress]
        [Required]
        [MaxLength(254)]
        [Display(Name = "Email address")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Pleace Enter your Age")]
        public string? Age { get; set; }

        [Required(ErrorMessage = "Pleace Enter your Address")]
        public string? Address { get; set; } 
      
    }
}
