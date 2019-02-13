using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sony.Core.Services.Contract;
using Sony.Models;

namespace Sony.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IItemService _itemService;

        public HomeController(IItemService itemService)
        {
            _itemService = itemService;
        }

        public IActionResult Index()
        {

            return View(_itemService.GetAll());
        }

        
    }
}
