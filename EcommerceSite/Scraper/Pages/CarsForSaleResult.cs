using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace EcommerceSite.Scraper.Pages
{
    public class CarsForSaleResult
    {
        IWebDriver driver;

        public CarsForSaleResult(IWebDriver driver)
        {
            this.driver = driver;
        }

        public List<String> getAllPremiumListingIds()
        {
            List<IWebElement> premiumListingIds = driver.FindElements(By.CssSelector("[data-qaid='cntnr-lstng-premium']")).ToList();

            List<String> listingIds = new List<String>();

            foreach (var element in premiumListingIds)
            {
                var id = element.GetAttribute("id");
                Console.WriteLine(id);
                listingIds.Add(id);
            }

            return listingIds;

        }

        public List<String> getAllStandardListingIds()
        {
            List<IWebElement> standardListingIds = driver.FindElements(By.CssSelector("[data-qaid='cntnr-lstng-standard']")).ToList();

            List<String> listingIds = new List<String>();

            foreach (var element in standardListingIds)
            {
                var id = element.GetAttribute("id");
                Console.WriteLine(id);
                listingIds.Add(id);
            }

            return listingIds;

        }

        public CarsForSaleResult goToNextPage()
        {
            IWebElement buttonNext = driver.FindElement(By.ClassName("glyphicon-menu-right"));

            buttonNext.Click();

            return this;
        }

        public bool existsNext()
        {

            var element = driver.FindElements(By.ClassName("glyphicon-menu-right")).Count >= 1 ? true : false;

     
            return element;
        }

      
    }
}
