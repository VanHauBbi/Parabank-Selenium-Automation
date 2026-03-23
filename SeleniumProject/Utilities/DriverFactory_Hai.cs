using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumProject.Utilities
{
    public class DriverFactory_Hai
    {
        public static IWebDriver InitDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--remote-allow-origins=*");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            IWebDriver driver = new ChromeDriver(options);

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");
            return driver;
        }
    }
}
