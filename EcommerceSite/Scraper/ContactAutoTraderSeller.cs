using EcommerceSite.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EcommerceSite.Scraper
{
    public class ContactAutoTraderSeller
    {
        private readonly ItemContext _context;

        public ContactAutoTraderSeller(ItemContext context)
        {
            _context = context;
        }

        public void contact()
        {
            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            ILogger logger = loggerFactory.CreateLogger<Program>();

            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("headless");
            //options.AddArgument("window-size-1200X600");
            options.AddArgument("--start-maximized");

            IWebDriver driver = new ChromeDriver(options);
        }

    }
}
