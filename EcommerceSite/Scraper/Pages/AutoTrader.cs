﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EcommerceSite.Scraper.Pages
{
    public class AutoTrader
    {
        private const string baseURL = "https://www.autotrader.com";
        private IWebDriver driver;

        public AutoTrader(IWebDriver driver)
        {
            this.driver = driver;
            driver.Url = baseURL;
        }

        public NavBar getNavBar()
        {
            return new NavBar(driver);
        }
    }
}
