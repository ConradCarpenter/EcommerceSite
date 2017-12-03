using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Models;
using EcommerceSite.Data;

namespace EcommerceSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ItemContext _context;

        public HomeController(ItemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Something Something about page";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Something Something Contact Page ";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        
    }
}
