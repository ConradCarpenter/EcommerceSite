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
    public class CarsForSale
    {
        private IWebDriver driver;

        public CarsForSale(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void SelectSearchRadius(String radius)
        {
            /*We might want to check the radius before we try to set it.*/

            SelectElement oSelect = new SelectElement(driver.FindElement(By.Name("searchRadius")));

            oSelect.SelectByValue(radius);
        }

        public void SelectMinPrice(String minPrice)
        {
            /*We might want to check the minPrice before we try to set it.*/

            SelectElement oSelect = new SelectElement(driver.FindElement(By.Name("minPrice")));

            oSelect.SelectByValue(minPrice);
        }

        public void SelectMaxPrice(String maxPrice)
        {
            /*We might want to check the maxPrice before we try to set it.*/

            SelectElement oSelect = new SelectElement(driver.FindElement(By.Name("maxPrice")));

            oSelect.SelectByValue(maxPrice);
        }

        public void EnterZipCode(String zip)
        {
            IWebElement inputBox = driver.FindElement(By.Name("zip"));

            inputBox.SendKeys(zip);
        }

        public void SelectCondition(String condition)
        {
            if (condition.ToLower() == "new")
            {
                IWebElement checkboxNew = driver.FindElement(By.XPath("//div[@data-reactid='158']"));

                checkboxNew.Click();

            }
            else if (condition.ToLower() == "used")
            {
                IWebElement checkboxUsed = driver.FindElement(By.XPath("//div[@data-reactid='163']"));
                checkboxUsed.Click();

            }
            else if (condition.ToLower() == "certified")
            {
                IWebElement checkboxCertified = driver.FindElement(By.XPath("//div[@data-reactid='168']"));
                checkboxCertified.Click();

            }
            else
            {
                /*If the user supplied an invalid condition do nothing*/
                return;
            }
        }

        public void SelectSellerType(String type)
        {
            if(type.ToLower() == "dealer")
            {
                IWebElement checkboxDealer = driver.FindElement(By.XPath("//div[@data-reactid='1134']"));

                checkboxDealer.Click();

            }
            else if(type.ToLower() == "private")
            {
                IWebElement checkboxPrivate = driver.FindElement(By.XPath("//div[@data-reactid='1141']"));

                checkboxPrivate.Click();
            }
            else
            {
                /*If the type is incorrect do nothing*/
                return;
            }
        }

        public CarsForSaleResult Search()
        {
            IWebElement buttonSearch = driver.FindElement(By.XPath("//button[@type='submit']"));

            buttonSearch.Click();

            return new CarsForSaleResult(driver);
;        }

    }
}
