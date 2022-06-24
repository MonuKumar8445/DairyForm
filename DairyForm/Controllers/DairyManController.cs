using BusinessAccessLayer.ProductInterface;
//using DairyForm.Services.BizObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Models.EntityModels;
using Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using static BusinessAccessLayer.ProductService.DairyManService;

namespace DairyForm.Controllers
{
    public class DairyManController : Controller
    {
        private readonly IPasswordHasher<DairyMan> _passwordHasher;
        private readonly IDairyMan _dairyManService;
        private readonly IWebHostEnvironment _env;
       

        public DairyManController(IPasswordHasher<DairyMan> passwordHasher, IWebHostEnvironment env, IDairyMan DairyManService/*, IEmailService emailService*/)
        {
            _passwordHasher = passwordHasher;
            _dairyManService = DairyManService;
            _env = env;
            //_emailService = emailService;
        }
        public IActionResult Index()
        {
            var dairyMan = _dairyManService.GetDairyMen();
            return View(dairyMan);
        }
        public async Task<IActionResult> GetDairyManById(string Id)
        {
            var result = await _dairyManService.GetDairyManById(Id);
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel createViewModel)
        {
            var result = await _dairyManService.CreateUserAsync(createViewModel);
            if (result)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "errorMessage Description");
            }

            return View(createViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            DairyMan user = await _dairyManService.GetDairyManById(Id);
            if (user! == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                CreateViewModel createViewModel = new CreateViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    Age = user.Age,
                    Password = user.PasswordHash


                };

                return View(createViewModel);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(CreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                DairyMan user = await _dairyManService.GetDairyManById(createViewModel.Id);
                if (user != null)
                {
                    user.Id = createViewModel.Id;
                    user.FirstName = createViewModel.FirstName;
                    user.LastName = createViewModel.LastName;
                    user.Email = createViewModel.Email;
                    user.Address = createViewModel.Address;
                    user.Age = createViewModel.Age;
                    user.PasswordHash = _passwordHasher.HashPassword(user, createViewModel.Password);
                    var result = await _dairyManService.UpdateAsync(user);
                    await _dairyManService.Save();
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ModelState.AddModelError("", "User not found");
                    }
                }
            }

            return View(createViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {

            DairyMan user = await _dairyManService.GetDairyManById(Id);
            if (user != null)
            {
                var result = await _dairyManService.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", "User Not Found");
                }

            }
            return View();
        }


        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _dairyManService.PasswordSignInAsync(loginViewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "DairyMan");
                }
                ModelState.AddModelError("", "Invalid Credeential");
            }
            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _dairyManService.CreateUserAsync(signUpViewModel);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "errorMessage Description");
                }
                ModelState.Clear();
            }

            return View(signUpViewModel);
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordViewModel { Token = token, Email = email };
           return View(model);
        }

        
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await _dairyManService.ResetPassword(model);
                if (result)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }


            }
            return View(model);
        }

        
        public IActionResult welcome()
        {
            return View();
        }


        [AllowAnonymous]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _dairyManService.SignOutAsync();
            return RedirectToAction("Login", "DairyMan");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (email != null)
            {
                var user = await _dairyManService.GetDairyManByEmail(email);
                if (user == null)
                {
                    TempData["SendMsg"] = "Invalid User";
                    return View();
                }
                string FullName = user.FirstName + " " + user.LastName;
                if (user != null)
                {
                    var token = await _dairyManService.GenerateForgotPasswordTokenAsync(user);
                    var path = _env.ContentRootPath + @"wwwroot\Templates\ForgotPassword.html";
                    var link = Url.Action("ResetPassword", "DairyMan", new { token, email }, Request.Scheme);
                    if (link != null)
                    {
                        bool emailResponse = _dairyManService.SendEmailPasswordResetLink(user.Email, FullName, link, path);
                        if (emailResponse == true)
                        {
                            TempData["SendMsgEmail"] = "Email Send successfully";
                            return View();
                        }
                        TempData["SendMsgEmail"] = "Email not send";
                        return View();
                    }
                }
                return View();
            }
            return View();

        }

    }
}

      

   

