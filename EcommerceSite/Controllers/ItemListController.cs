using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Models;

namespace EcommerceSite.Controllers
{
    public class ItemListController : Controller
    {
        private readonly IRepo AllItems; 
        public IActionResult FullList()
        {
            var data = new TestData();
            ViewBag.message = "Welcome to our Item Page";
            return View(data.Items);
        }
    }
}