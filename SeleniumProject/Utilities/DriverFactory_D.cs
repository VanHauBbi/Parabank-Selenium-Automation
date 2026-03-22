using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Utilities
{
    public class DriverFactory_D
    {
        public static IWebDriver InitDriver()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(
                "https://parabank.parasoft.com/parabank/index.htm"
            );

            return driver;
        }
    }
}
