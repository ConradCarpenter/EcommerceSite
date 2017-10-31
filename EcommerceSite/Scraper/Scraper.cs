using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using EcommerceSite.Models;


namespace EcommerceSite.Scraper
{
    public class WebsiteScraper
    {
        public void Scrape()
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("headless");
            //options.AddArgument("window-size-1200X600");

            IWebDriver driver = new ChromeDriver(options);

            Pages.AutoTrader home = new Pages.AutoTrader(driver);

            Pages.NavBar navbar = home.getNavBar();

            Pages.CarsForSale carsforsale = navbar.GoToCarsForSale();

            carsforsale.SelectSearchRadius("25");

            carsforsale.EnterZipCode("06457");

            carsforsale.SelectCondition("used");

            carsforsale.SelectMaxPrice("10000");

            carsforsale.SelectSellerType("private");

            Pages.CarsForSaleResult results = carsforsale.Search();

            //This list will hold all listing ids (premium and standard)
            List<String> listingIds = new List<String>();
            int i = 0;
            do
            {
                if (i != 0)
                {
                    results = results.GoToNextPage();
                }

                //Get Premium Listings
                var plist = results.GetAllPremiumListingIds();

                //Get Standard Listings
                var slist = results.GetAllStandardListingIds();

                //Add Premium and Standard listings to the main list
                listingIds.AddRange(plist);
                listingIds.AddRange(slist);

                i++;

            } while(results.ExistsNext());


            foreach (String listingId in listingIds)
            {

                try
                {
                   
                    Item listing = new Item();

                    Pages.AutoTraderDetail detail = new Pages.AutoTraderDetail(driver);
                    detail.NavigateListing(listingId);
                    listing.ImageURL = detail.GetMainPhoto();
                    listing.Name = detail.GetTitle();

                    // Upload Photo to S3
                    // Get URL Back
                    // Save to DB
                    // listing.save!
                }
                catch (NoSuchElementException)
                {
                    // Email Developer...
                }
            }

             
        }
    }
}
