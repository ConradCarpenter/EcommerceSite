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

            logger.LogInformation("Starting AutoTrader.com scrapper");

            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("headless");
            //options.AddArgument("window-size-1200X600");
            options.AddArgument("--start-maximized");

            IWebDriver driver = new ChromeDriver(options);

            Pages.AutoTrader home = new Pages.AutoTrader(driver);

            Pages.NavBar navbar = home.getNavBar();

            Pages.CarsForSale carsforsale = navbar.GoToCarsForSale();

            logger.LogInformation("Opened Carsforsale");

            carsforsale.SelectSearchRadius("25");

            logger.LogInformation("Entered Search Radius");

            carsforsale.EnterZipCode("06457");

            logger.LogInformation("Entered ZipCode");

            carsforsale.SelectCondition("used");

            logger.LogInformation("Selected Condition");

            carsforsale.SelectMaxPrice("10000");

            logger.LogInformation("Selected Max Price");

            carsforsale.SelectSellerType("private");

            logger.LogInformation("Selected Seller Type");

            Pages.CarsForSaleResult results = carsforsale.Search();

            logger.LogInformation("Search result returned");


            //This list will hold all listing ids (premium and standard)
            List<String> listingIds = new List<String>();
            int i = 0;
            do
            {
                logger.LogInformation("Scraping Page: " + i.ToString());

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

                //Get Center Listing
                var clist = results.GetAllCenterListingIds();
                logger.LogInformation("Number of clist: " + clist.Count.ToString());

                //Add Premium/Featured/Standard listings to the main list
                listingIds.AddRange(plist);
                listingIds.AddRange(flist);
                listingIds.AddRange(slist);
                listingIds.AddRange(clist);

                i++;


            } while (results.ExistsNext());

            logger.LogInformation(String.Join(":", listingIds));

            _context.Database.EnsureCreated();

           
         
            foreach (String listingId in listingIds)
            {

                try
                {

                    logger.LogInformation("Scraping Listing Id:" + listingId);

                    //Here we should remove the listingIds already in the database so we do not double scrape.

                    List<Item> it = _context.Items.Where(p => p.ForeignListingId == listingId).ToList();

                    logger.LogInformation(it.ToString());

                    logger.LogInformation("Retrieved listing foreign id: " + it[0].ForeignListingId.ToString()); 

                    if (it.Count > 0)
                    {
                        logger.LogInformation("Adding new item:" + listingId);
                        Item listing = new Item();

                        Pages.AutoTraderDetail detail = new Pages.AutoTraderDetail(driver, _context);
                        detail.NavigateListing(listingId);
                        listing.ImageURL = detail.GetMainPhoto();
                        listing.Name = detail.GetTitle();
                        listing.Price = detail.GetPrice();
                        listing.Desc = detail.GetDescription();
                        listing.ForeignListingId = listingId;

                        _context.Items.Add(listing);

                        _context.SaveChanges();

                    }
                    else
                    {
                        logger.LogInformation("Not adding item: " + listingId + " it already exits");
                    }



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
