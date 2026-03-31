using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ParaBankAutomation.Utilities
{
    public static class DriverFactory
    {
        private static IWebDriver _driver;

        public static IWebDriver GetDriver()
        {
            if (_driver == null)
            {
                var options = new ChromeOptions();
                options.AddArgument("--start-maximized"); 
                _driver = new ChromeDriver(options);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            return _driver;
        }

        public static void QuitDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null; 
            }
        }
    }
}