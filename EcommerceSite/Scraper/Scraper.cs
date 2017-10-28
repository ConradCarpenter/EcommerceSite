using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using EcommerceSite.Models;


namespace EcommerceSite.Scraper
{
    class Scraper
    {
        static void Main(string[] args)
        {

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            options.AddArgument("window-size-1200X600");
           
            
            Pages.AutoTrader home = new Pages.AutoTrader(new ChromeDriver(options));

            Pages.NavBar navbar = home.getNavBar();

            Pages.CarsForSale carsforsale = navbar.goToCarsForSale();

            carsforsale.selectSearchRadius("25");

            carsforsale.enterZipCode("06457");

            carsforsale.selectCondition("used");

            carsforsale.selectMaxPrice("20000");

            carsforsale.selectSellerType("dealer");

            Pages.CarsForSaleResult results = carsforsale.search();

            //This list will hold all listing ids (premium and standard)
            List<String> listingIds = new List<String>();
            int i = 0;
            do
            {
                if (i != 0)
                {
                    results = results.goToNextPage();
                }

                //Get Premium Listings
                var plist = results.getAllPremiumListingIds();

                //Get Standard Listings
                var slist = results.getAllStandardListingIds();

                //Add Premium and Standard listings to the main list
                listingIds.AddRange(plist);
                listingIds.AddRange(slist);

                i++;

            } while(results.existsNext());
            //Console.Write(x);

            // get array of listing ids
            // AutoTraderIndex.getListings();

            // For each listing...
            /*
            try {
				int listingId = 456883010; // Test ID
				Item listing = new Item();

				Pages.AutoTraderDetail detail = new Pages.AutoTraderDetail(new ChromeDriver());
				detail.NavigateListing(listingId);
				listing.ImageURL = detail.GetMainPhoto();
				listing.Name = detail.GetTitle();

                // Upload Photo to S3
                // Get URL Back
                // Save to DB
                // listing.save!
            } catch (NoSuchElementException) {
                // Email Developer...
            }

             */
        }
    }
}
