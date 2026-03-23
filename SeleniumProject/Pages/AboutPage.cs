using OpenQA.Selenium;
using SeleniumProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Pages
{
    public class AboutPage
    {
        private readonly IWebDriver _driver;

        public AboutPage()
        {
            _driver = DriverFactory_Toan.GetDriver();
        }

        private IWebElement AboutUsLink => _driver.FindElement(By.LinkText("About Us"));
        private IWebElement AboutUsTitle => _driver.FindElement(By.ClassName("title"));
        private IWebElement SolutionLink => _driver.FindElement(By.LinkText("www.parasoft.com"));

        public void NavigateTo()
        {
            AboutUsLink.Click();
        }

        public string GetPageTitle()
        {
            return AboutUsTitle.Text;
        }

        public string GetSolutionLinkUrl()
        {
            return SolutionLink.GetAttribute("href");
        }
    }

}
