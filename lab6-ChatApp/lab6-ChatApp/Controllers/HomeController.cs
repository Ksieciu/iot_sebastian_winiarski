using lab6_ChatApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab6_ChatApp.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private const string UsernameKey = "USER_NAME_KEY";
        
        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            return View(new SignInViewModel());
        }

        [HttpPost("signin")]
        public IActionResult SignIn(SignInViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            HttpContext.Session.SetString(UsernameKey, vm.Username);

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString(UsernameKey);

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("SignIn");
            }

            ViewData["Username"] = username;

            return View();
        }
    }
}
