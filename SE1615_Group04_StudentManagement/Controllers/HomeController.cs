using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SE1615_Group04_StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SE1615_Group04_StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        StudentManagermentContext context;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            context = new StudentManagermentContext();
        }
        
        public IActionResult Index()
            {
                return View();
            }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(IFormCollection values)
        {
            string name, pass;
            name = values["UserName"];
            pass = values["Password"];
            
            Account account = context.Accounts
                .Where(a => a.Username == name
                && a.Password == pass)
                .FirstOrDefault();

            if (account != null)
            {

                HttpContext.Session.SetString("UserName", name);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Error"] = "Can't find that member!";
                return View();
            }

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
