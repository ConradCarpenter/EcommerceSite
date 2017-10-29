using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EcommerceSite.Scraper;

namespace EcommerceSite.Controllers
{
    public class ScrapeController : Controller
    {
        public IActionResult AutoTrader()
        {
            WebsiteScraper Scraper = new WebsiteScraper();
            Scraper.Scrape();
            // Won't return anything yet, because we need a job worker to not block
            // this thread!
            return StatusCode(200);
        }
    }
}
