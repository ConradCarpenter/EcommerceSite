using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Models;
using EcommerceSite.Data;

namespace EcommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ItemContext _context;

        public CartController(ItemContext context)
        {
            _context = context;
        }
        public IActionResult Total()
        {
            List<Item> cartItems = new List<Item>();

            List<Item> dataItem = _context.Items.ToList();
            string cookies = Request.Cookies["cart"];
            string[] items = cookies.Split('-');
            foreach (var item in items)
            {
                foreach(var i in dataItem)
                {
                    if (item == i.Name) {
                        cartItems.Add(i);
                        break; 
                    } 
                }
            }
            double total = 0;
            foreach (var item in cartItems)
            {
                total += item.Price;
            }
            ViewBag.Total = total; 
            return View(cartItems);
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}