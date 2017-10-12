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
            // get array of listing ids
            // AutoTraderIndex.getListings();

            // For each listing...
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
        }
    }
}
