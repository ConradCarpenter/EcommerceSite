using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Models;

namespace EcommerceSite.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            TestData data = new TestData();
            return View(data.Items);
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
