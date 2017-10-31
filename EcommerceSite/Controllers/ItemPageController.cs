using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Models;
using EcommerceSite.Data;

namespace EcommerceSite.Controllers
{
    public class ItemPageController : Controller
    {
        private readonly ItemContext _context;

        public ItemPageController(ItemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Item(int itemN)
        {
            var Items = _context.Items.ToList();

            foreach (var item in Items)
            {
                if (item.ItemNumber == itemN)
                {
                    return View(item);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult Item(string name)
        {
            if(Request.Cookies["cart"] != null) Response.Cookies.Append("cart", Request.Cookies["cart"] + "-" + name);
            else Response.Cookies.Append("cart", name);

            ViewBag.Message = "Item added to cart";
            return RedirectToAction("Total","Cart");
        }
    }
}