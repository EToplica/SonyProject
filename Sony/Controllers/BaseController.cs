using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sony.Models;

namespace Sony.Controllers
{

    public class BaseController : Controller
    {
        protected readonly UserManager<ApplicationUser> _userManager;


        public BaseController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected string UserId { get { return _userManager.GetUserAsync(User).Result.Id; } }

        public IActionResult Index()
        {

            return View();
        }
    }
}