using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using EcommerceSite.Data;
using Microsoft.Extensions.Logging;



namespace EcommerceSite.Scraper.Pages
{
    public class AutoTraderDetail
    {
        IWebDriver driver;
        const string baseURL = "https://www.autotrader.com/cars-for-sale/vehicledetails.xhtml?listingId=";

        private readonly ItemContext _context;

        public AutoTraderDetail(IWebDriver driver, ItemContext context)
        {
            this.driver = driver;
            this._context = context;
        }

        public void NavigateListing(String listingId) {
            driver.Url = baseURL + listingId;
        }

        public Double GetPrice()
        {
           
            IWebElement price = driver.FindElement(By.CssSelector("[data-qaid='cntnr-lstng-price1']"));
            
            return Convert.ToDouble(price.Text.Substring(1));
        }

        public String GetMainPhoto() {
            IWebElement picture = driver.FindElement(By.XPath("//div[contains(@class, 'media-viewer')]//img"));
            return picture.GetAttribute("src");
        }

        public String GetTitle() {
            IWebElement title = driver.FindElement(By.XPath("//div[@data-qaid='cntnr-vehicle-title-header']"));
            return title.Text;
        }

        public String GetDescription()
        {
            IWebElement description = driver.FindElement(By.CssSelector("[data-qaid='cntnr-listingDescription']"));
            return description.Text;
        }
    }

}
