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
        IWebDriver driver;

        public CarsForSale(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void selectSearchRadius(String radius)
        {
            /*We might want to check the radius before we try to set it.*/

            SelectElement oSelect = new SelectElement(driver.FindElement(By.Name("searchRadius")));

            oSelect.SelectByValue(radius);
        }

        public void selectMinPrice(String minPrice)
        {
            /*We might want to check the minPrice before we try to set it.*/

            SelectElement oSelect = new SelectElement(driver.FindElement(By.Name("minPrice")));

            oSelect.SelectByValue(minPrice);
        }

        public void selectMaxPrice(String maxPrice)
        {
            /*We might want to check the maxPrice before we try to set it.*/

            SelectElement oSelect = new SelectElement(driver.FindElement(By.Name("maxPrice")));

            oSelect.SelectByValue(maxPrice);
        }

        public void enterZipCode(String zip)
        {
            IWebElement inputBox = driver.FindElement(By.Name("zip"));

            inputBox.SendKeys(zip);
        }

        public void selectCondition(String condition)
        {
            if (condition.ToLower() == "new")
            {
                IWebElement checkboxNew = driver.FindElement(By.XPath("//div[@data-reactid='159']"));

                checkboxNew.Click();

            }
            else if (condition.ToLower() == "used")
            {
                IWebElement checkboxUsed = driver.FindElement(By.XPath("//div[@data-reactid='164']"));
                checkboxUsed.Click();

            }
            else if (condition.ToLower() == "certified")
            {
                IWebElement checkboxCertified = driver.FindElement(By.XPath("//div[@data-reactid='169']"));
                checkboxCertified.Click();

            }
            else
            {
                /*If the user supplied an invalid condition do nothing*/
                return;
            }
        }

        public void selectSellerType(String type)
        {
            if(type.ToLower() == "dealer")
            {
                IWebElement checkboxDealer = driver.FindElement(By.XPath("//div[@data-reactid='1135']"));

                checkboxDealer.Click();

            }
            else if(type.ToLower() == "private")
            {
                IWebElement checkboxPrivate = driver.FindElement(By.XPath("//div[@data-reactid='1142']"));

                checkboxPrivate.Click();
            }
            else
            {
                /*If the type is incorrect do nothing*/
                return;
            }
        }

        public CarsForSaleResult search()
        {
            IWebElement buttonSearch = driver.FindElement(By.XPath("//button[@type='submit']"));

            buttonSearch.Click();

            return new CarsForSaleResult(driver);
;        }

    }
}
