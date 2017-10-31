using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Models;
using EcommerceSite.Data;

namespace EcommerceSite.Controllers
{
    [Route("Items")]
    public class ItemListController : Controller
    {
        private readonly ItemContext _context;

        public ItemListController(ItemContext context)
        {
            _context = context;
        }

        public IActionResult FullList()
        {
            ViewBag.message = "Welcome to our Item Page";
            return View(_context.Items.ToList());
        }
    }
}