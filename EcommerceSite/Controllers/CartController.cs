using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Models;
using EcommerceSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EcommerceSite.Scraper;
using Hangfire;

namespace EcommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ItemContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CartController(ItemContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            List<Item> cartItems = new List<Item>();

            List<Item> dataItem = _context.Items.ToList();
            string cookies = Request.Cookies["cart"];
            string[] items = cookies.Split('-');
            foreach (var item in items)
            {
                foreach (var i in dataItem)
                {
                    if (item == i.Name)
                    {
                        cartItems.Add(i);

                        ContactAutoTraderSeller Contacter = new ContactAutoTraderSeller(_context);

                        var tmpUser = await _userManager.GetUserAsync(User);
                        var email = tmpUser.Email;

                        BackgroundJob.Enqueue(() =>Contacter.contact(i.ForeignListingId,email));

                        break;
                    }
                }
            }

            return View();
        }
    }
}