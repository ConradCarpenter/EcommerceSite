using EcommerceSite.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Hangfire;

namespace EcommerceSite.Scraper
{
    public class ContactAutoTraderSeller
    {
        private readonly ItemContext _context;

        public ContactAutoTraderSeller(ItemContext context)
        {
            _context = context;
        }
        [AutomaticRetry(Attempts = 0)]
        public void contact(String listingId, String Email)
        {
            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            ILogger logger = loggerFactory.CreateLogger<Program>();

            String firstName = "Another";
            String lastName = "Treasure";
            String message = "Our user is interested in the item. Please contact them via EMAIL";


            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("headless");
            //options.AddArgument("window-size-1200X600");
            options.AddArgument("--start-maximized");

            IWebDriver driver = new ChromeDriver(options);

            Pages.AutoTraderDetail details = new Pages.AutoTraderDetail(driver, _context);

            /*Navigate to listing*/
            details.NavigateListing(listingId);

            /* Set the First Name in the contact form*/
            details.SetFirstName(firstName);

            /* Set the Last Name in the contact form*/
            details.SetLastName(lastName);

            /* Set the Message in the contact form*/
            details.setMessage(message);

            /* Set the Email in the contact form*/
            details.SetEmail(Email);

            // Keep this off so that you don't spam people
            //details.SubmitContactInfo();

            /* Close the browser window*/
            driver.Close();

        }

    }
}
