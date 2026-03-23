using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumProject.Utilities
{
    public class DriverFactory
    {
        public IWebDriver Driver { get; private set; } = null!;

        public void InitDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            options.AddArgument("--log-level=3");
            options.AddArgument("--silent");
            options.AddArgument("--disable-logging");

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();

            service.HideCommandPromptWindow = true;

            service.SuppressInitialDiagnosticInformation = true;

            Driver = new ChromeDriver(service, options);

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void CloseDriver()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
            }
        }
    }
}