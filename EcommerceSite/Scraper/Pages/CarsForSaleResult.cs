using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Microsoft.Extensions.Logging;

namespace EcommerceSite.Scraper.Pages
{
    public class CarsForSaleResult
    {
        private IWebDriver driver;

        public CarsForSaleResult(IWebDriver driver)
        {
            this.driver = driver;
        }

        public List<String> GetAllPremiumListingIds()
        {
            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            ILogger logger = loggerFactory.CreateLogger<Program>();


            List<IWebElement> premiumListingIds = driver.FindElements(By.CssSelector("[data-qaid='cntnr-lstng-premium']")).ToList();

            List<String> listingIds = new List<String>();

            foreach (var element in premiumListingIds)
            {
                var id = element.GetAttribute("id");
                logger.LogInformation("Found Premium Listing Id: " + id.ToString());
                listingIds.Add(id);
            }

            return listingIds;

        }

        public List<String> GetAllFeaturedListingIds()
        {
            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            ILogger logger = loggerFactory.CreateLogger<Program>();


            List<IWebElement> featuredListingIds = driver.FindElements(By.CssSelector("[data-qaid='cntnr-lstng-featured']")).ToList();

            List<String> listingIds = new List<String>();

            foreach (var element in featuredListingIds)
            {
                var id = element.GetAttribute("id");
                logger.LogInformation("Found Featured Listing Id: " + id.ToString());
                listingIds.Add(id);
            }

            return listingIds;

        }

        public List<String> GetAllStandardListingIds()
        {
            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            ILogger logger = loggerFactory.CreateLogger<Program>();

            List<IWebElement> standardListingIds = driver.FindElements(By.CssSelector("[data-qaid='cntnr-lstng-standard']")).ToList();

            List<String> listingIds = new List<String>();

            foreach (var element in standardListingIds)
            {
                var id = element.GetAttribute("id");
                logger.LogInformation("Found Standard Listing Id: " + id.ToString());
                listingIds.Add(id);
            }

            return listingIds;

        }

        public List<String> GetAllCenterListingIds()
        {

            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            ILogger logger = loggerFactory.CreateLogger<Program>();

            List<IWebElement> cntrListings = new List<IWebElement>();

            for(var i = 0; i<10; i ++)
            {
                String selector = "div[data-qaid *= \"cntnr-lstng-" + i.ToString() + "\"]";

                //logger.LogInformation("selector: " + selector);

                List<IWebElement> tmp = driver.FindElements(By.CssSelector(selector)).ToList();

                cntrListings.AddRange(tmp);
            }

            List<String> listingIds = new List<String>();

            foreach (var element in cntrListings)
            {
                var id = element.GetAttribute("id");
                logger.LogInformation("Found Center Listing Id: " + id.ToString());
                listingIds.Add(id);
            }

            return listingIds;


        }

        public CarsForSaleResult GoToNextPage()
        {
            IWebElement buttonNext = driver.FindElement(By.ClassName("glyphicon-menu-right"));

            buttonNext.Click();

            return this;
        }

        public bool ExistsNext()
        {


            var element = driver.FindElements(By.ClassName("glyphicon-menu-right")).Count >= 2 ? true : false;

            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            ILogger logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("number of glyphicon-menu-right elemnets" + driver.FindElements(By.ClassName("glyphicon-menu-right")).Count.ToString());

            return element;
        }


    }
}
