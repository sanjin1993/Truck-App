using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Truck_Web.Models;
using Truck_Web.Services.IRepository;
using Truck_Web.ViewModel;

namespace Truck_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuditFileRepository _auditFileRepository;



        public HomeController(ILogger<HomeController> logger, ITimeSheetRepository timeSheetRepository, IAuditFileRepository auditFileRepository, IAccountRepository accountRepository)
        {
            _logger = logger;
            _timeSheetRepository = timeSheetRepository;
            _auditFileRepository = auditFileRepository;
            _accountRepository = accountRepository;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel listOfSheetAndAudit = new IndexViewModel()
            {
                TimeSheetList = await _timeSheetRepository.GetAllAsync(SD.TimeSheetAPIPath, HttpContext.Session.GetString("JWToken")),
                AuditFileList = await _auditFileRepository.GetAllAsync(SD.AuditFilePath, HttpContext.Session.GetString("JWToken"))
            };

            if (HttpContext.Session.GetString("JWToken") == null || HttpContext.Session.GetString("JWToken").ToString().Length <= 0)
            {
                return RedirectToAction("Login");
            }

            return View(listOfSheetAndAudit);
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

        [HttpGet]
        public IActionResult Login()
        {
            User obj = new User();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User obj)
        {
            User objUser = await _accountRepository.LoginAsync(SD.AccountPath + "authenticate/", obj);
            if (objUser.Token == null)
            {
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objUser.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, objUser.Role));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("JWToken", objUser.Token);
            TempData["alert"] = "Welcome " + objUser.Username;
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User obj)
        {
            bool result = await _accountRepository.RegisterAsync(SD.AccountPath + "register/", obj);
            if (result == false)
            {
                return View();
            }
            TempData["alert"] = "Registration Successful";
            return RedirectToAction("Login");

        }

        public async Task<IActionResult> Logout(User obj)
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult View1()
        {
            return View();
        }
    }
}
