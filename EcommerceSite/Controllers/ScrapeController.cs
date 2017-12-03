using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Scraper;
using Hangfire;
using EcommerceSite.Data;

namespace EcommerceSite.Controllers
{
    public class ScrapeController : Controller
    {
        private readonly ItemContext _context;

        public ScrapeController(ItemContext context)
        {
            _context = context;
        }

        public IActionResult AutoTrader()
        {
            WebsiteScraper Scraper = new WebsiteScraper(_context);
            BackgroundJob.Enqueue(() => Scraper.Scrape());

            return StatusCode(200);
        }
    }
}
