
using Microsoft.AspNetCore.Identity;
using Models.EntityModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.ProductInterface
{
    public interface IDairyMan
    {
        IEnumerable<DairyMan> GetDairyMen();
        Task<bool> CreateUserAsync(CreateViewModel dairyMan);
        Task<DairyMan> GetDairyManById(string Id);
        Task<DairyMan> GetDairyManByEmail(string email);

        Task<IdentityResult> UpdateAsync(DairyMan model);
        Task<IdentityResult> DeleteAsync(DairyMan dairyMan);
        Task<SignInResult> PasswordSignInAsync(LoginViewModel loginViewModel);
        Task<IdentityResult> CreateUserAsync(SignUpViewModel dairyMan);
        Task<bool> ResetPassword(ResetPasswordViewModel model);
        bool SendEmailPasswordResetLink(string Email, string fullName, string link, string localPath);
        Task<string> GenerateForgotPasswordTokenAsync(DairyMan dairyMan);
        Task SignOutAsync();
        Task<IdentityResult> Save();
       
        //Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model, string Id);
    }
}
