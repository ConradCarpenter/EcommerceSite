using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using EcommerceSite.Models;
using EcommerceSite.Data;
using Microsoft.Extensions.Logging;


namespace EcommerceSite.Scraper
{
    public class WebsiteScraper
    {
        private readonly ItemContext _context;

        public WebsiteScraper(ItemContext context)
        {
            _context = context;
        }

        public void Scrape()
        {

            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            ILogger logger = loggerFactory.CreateLogger<Program>();
          


            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("headless");
            //options.AddArgument("window-size-1200X600");
            options.AddArgument("--start-maximized");

            IWebDriver driver = new ChromeDriver(options);

            

            Pages.AutoTrader home = new Pages.AutoTrader(driver);

            Pages.NavBar navbar = home.getNavBar();

            Pages.CarsForSale carsforsale = navbar.GoToCarsForSale();

            carsforsale.SelectSearchRadius("25");

            carsforsale.EnterZipCode("06457");

            carsforsale.SelectCondition("used");

            carsforsale.SelectMaxPrice("2000");

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

                logger.LogInformation("Number of plist: " + plist.Count.ToString());

                //Get Featured Listings
                var flist = results.GetAllFeaturedListingIds();

                logger.LogInformation("Number of flist: " + flist.Count.ToString());

                
                //Get Standard Listings
                var slist = results.GetAllStandardListingIds();

                logger.LogInformation("Number of slist: " + slist.Count.ToString());

                //Add Premium/Featured/Standard listings to the main list
                listingIds.AddRange(plist);
                listingIds.AddRange(flist);
                listingIds.AddRange(slist);

                i++;

            } while(results.ExistsNext());

            logger.LogInformation(String.Join(":",listingIds));

            _context.Database.EnsureCreated();

            foreach (String listingId in listingIds)
            {

                try
                {
                   
                    Item listing = new Item();

                    Pages.AutoTraderDetail detail = new Pages.AutoTraderDetail(driver,_context);
                    detail.NavigateListing(listingId);
                    listing.ImageURL = detail.GetMainPhoto();
                    listing.Name = detail.GetTitle();
                    listing.Price = detail.GetPrice();
                    listing.Desc = detail.GetDescription();

                    _context.Items.Add(listing);

                    _context.SaveChanges();

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
