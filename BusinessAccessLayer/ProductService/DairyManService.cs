using BusinessAccessLayer.ProductInterface;
using Microsoft.AspNetCore.Identity;

using Models.EntityModels;
using Models.ViewModels;
using System.Net;
using System.Net.Mail;

namespace BusinessAccessLayer.ProductService
{
    public class DairyManService : IDairyMan
    {
        public UserManager<DairyMan> userManager;
        public SignInManager<DairyMan> signInManager;
       // private object link;

        //private object _smtpConfig;

        //public string UserName { get; private set; }

        //private readonly IEmailService _emailService;



        public DairyManService(UserManager<DairyMan> UserManager, 
            SignInManager<DairyMan> _signInManager/*,IEmailService emailService*/)
        {
            userManager = UserManager;
            signInManager = _signInManager;
            //_emailService = emailService;
        }
        public IEnumerable<DairyMan> GetDairyMen()
        {
            List<DairyMan> model = userManager.Users.ToList();
            return model;
        }

        public async Task<DairyMan> GetDairyManById(string Id)
        {
            var result = await userManager.FindByIdAsync(Id);
            return result;
        }
        public async Task<DairyMan> GetDairyManByEmail(string email)
        {
            var result = await userManager.FindByEmailAsync(email);
            return result;
        }
        public async Task<IdentityResult> UpdateAsync(DairyMan model)
        {
            var dairyMan = await userManager.UpdateAsync(model);

            return dairyMan;

        }
        public async Task<IdentityResult> DeleteAsync(DairyMan dairyMan)
        {
            var result = await userManager.DeleteAsync(dairyMan);

            return result;

        }

        public async Task<IdentityResult> Save()
        {
            return IdentityResult.Success;
        }

        public async Task<bool> CreateUserAsync(CreateViewModel dairyMan)
        {
            DairyMan user = new DairyMan()
            {

                UserName = dairyMan.Email,
                FirstName = dairyMan.FirstName,
                LastName = dairyMan.LastName,
                Email = dairyMan.Email,
                Age = dairyMan.Age,
                Address = dairyMan.Address,
                Create_By = "rohal",
                Modified_By = "suman",

            };
            var result = await userManager.CreateAsync(user, dairyMan.Password);
            return result.Succeeded;
        }

        public async Task<SignInResult> PasswordSignInAsync(LoginViewModel loginViewModel)
        {
            var result = await signInManager.PasswordSignInAsync
                (loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, false);
            return result;
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpViewModel signUpView)
        {
            DairyMan user = new DairyMan()
            {

                UserName = signUpView.Email,
                FirstName = signUpView.FirstName,
                LastName = signUpView.LastName,
                Email = signUpView.Email,
                Age = signUpView.Age,
                Address = signUpView.Address,

                Create_By = "rohal",
                Modified_By = "suman",

            };

            var result = await userManager.CreateAsync(user, signUpView.Password);
            return result;
        }
    
        public async Task<bool> ResetPassword(ResetPasswordViewModel model)
        {
            if (model.Email != null)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await userManager.ResetPasswordAsync(user, token, model.Password);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public async Task<string> GenerateForgotPasswordTokenAsync(DairyMan dairyMan)
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(dairyMan);
            if (string.IsNullOrEmpty(token))
            {
              //await SendEmailPasswordReset(dairyMan, token,link);
            }
            return token;
        }



        public bool SendEmailPasswordResetLink(string Email, string fullName, string link, string localPath)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(localPath))
                {
                    body = reader.ReadToEnd();
                    body = body.Replace("{Link}", link);
                    body = body.Replace("{FirstName}", fullName);
                    //body = body.Replace("{Code}",code.ToString());
                }
                MailMessage message = new MailMessage();
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                message.From = new MailAddress("testdevchetu@gmail.com");
                message.To.Add(new MailAddress(Email));
                message.Subject = "Password Reset Request";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; 
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("testdevchetu@gmail.com", "Chetu@123");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;

        }



        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

       
    }
}
