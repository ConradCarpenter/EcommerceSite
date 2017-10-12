using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EcommerceSite.Scraper.Pages
{
    public class AutoTraderDetail
    {
        IWebDriver driver;
        const string baseURL = "https://www.autotrader.com/cars-for-sale/vehicledetails.xhtml?listingId=";

        public AutoTraderDetail(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void NavigateListing(int listingId) {
            driver.Url = baseURL + listingId;
        }

        public String GetMainPhoto() {
            IWebElement picture = driver.FindElement(By.XPath("//div[contains(@class, 'media-viewer')]//img"));
            return picture.GetAttribute("src");
        }

        public String GetTitle() {
            IWebElement title = driver.FindElement(By.XPath("//div[@data-qaid='cntnr-vehicle-title-header']"));
            return title.Text;
        }
    }
}
