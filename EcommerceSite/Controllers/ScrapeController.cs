using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Scraper;
using Hangfire;

namespace EcommerceSite.Controllers
{
    public class ScrapeController : Controller
    {
        public IActionResult AutoTrader()
        {
            WebsiteScraper Scraper = new WebsiteScraper();
            BackgroundJob.Enqueue(() => Scraper.Scrape());

            return StatusCode(200);
        }
    }
}
