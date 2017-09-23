using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Models;

namespace EcommerceSite.Controllers
{
    public class ItemPageController : Controller
    {
        [HttpGet]
        public IActionResult Item(int itemN)
        {
            var list = new TestData();
            var Items = list.Items;
            var i = new Item();
            
            foreach(var item in Items)
            {
                if (item.ItemNumber == itemN)
                {
                    return View(item);
                }
            }
            return View("Error");
        }
    }
}