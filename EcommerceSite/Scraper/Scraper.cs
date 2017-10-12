using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EcommerceSite
{
    class Scraper
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();

            driver.Url = "https://www.autotrader.com/";

            IWebElement element_CarsForSale = driver.FindElement(By.XPath("//a[@href='/cars-for-sale/?Log=0']"));

            element_CarsForSale.Click();

            //IWebElement element_SearchRadius = driver.FindElement(By.Name("searchRadius"));

            //IWebElement element_ZipCode = driver.FindElement(By.Name("zip"));

            //IWebElement element_MinimumPrice = driver.FindElement(By.Id("minPrice"));

            //IWebElement element_MaximumPrice = driver.FindElement(By.Id("maxPrice"));

        }
    }
}
