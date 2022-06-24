using DairyForm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System.Diagnostics;

namespace DairyForm.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger/*, IEmailService emailService*/)
        {
            _logger = logger;
            //_emailService = emailService;
        }

        public    IActionResult Index()
        {
            //UserEmailOption option = new UserEmailOption()
            //{
            //    ToEmails = new List<string> { "monuk@chetu.com"},
            //    PlaceHolders = new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{UserName}}","Amit")
            //    }
            //};
            //await _emailService.SendTestEmail(option);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}