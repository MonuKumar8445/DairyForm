using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class SignUpViewModel
    {
        //public string? Id { get; set; }
        [Required(ErrorMessage = "Pleace Enter your FirstName")]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "FirstName")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Pleace Enter your LastName")]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "LastName")]
        public string? LastName { get; set; }
        [Required(ErrorMessage ="Pleace Enter your Email")]
        [Display(Name ="Email Address")]
        [EmailAddress(ErrorMessage ="Pleace Enter a valid Eamil")]
        [MaxLength(254)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Pleace Enter your Password")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Pleace Confirm your password")]
        [Display(Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Pleace Enter your Age")]
        public string? Age { get; set; }
        [Required(ErrorMessage = "Pleace Enter your Address")]
        public string? Address { get; set; }
    }
}
