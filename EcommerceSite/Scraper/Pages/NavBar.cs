using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace EcommerceSite.Scraper.Pages
{
    public class NavBar
    {


        IWebDriver driver;

        public NavBar(IWebDriver driver)
        {
            this.driver = driver;
            
        }

        //[FindsBy(How=How.CssSelector,Using = "//a[@href='/cars-for-sale/?Log=0']")]
        //private IWebElement x { get; set; }

        public CarsForSale goToCarsForSale()
        {
            IWebElement buttonCarsForSale = driver.FindElement(By.XPath("//a[@href='/cars-for-sale/?Log=0']"));

            buttonCarsForSale.Click();

            return new CarsForSale(driver);

        }


    }
}
