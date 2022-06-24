using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required,DataType (DataType.Password),Display(Name ="Current Password")]
        public string? CurrentPassword { get; set; }
        public string? userId { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string? NewPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Confirm New Password")]
        //[Combiner("NewPassword",ErrorMessage="Confirm New Password does  not match")]
        public string? ConfirmNewpassword { get; set; }
    }
}
