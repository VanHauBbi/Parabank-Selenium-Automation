using OpenQA.Selenium;
using SeleniumProject.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Pages
{
    public class ServicesPage
    {
        private readonly IWebDriver _driver;
        public ServicesPage() => _driver = DriverFactory_Toan.GetDriver();

        // Selector thực tế trên ParaBank
        private IWebElement ServicesLink => _driver.FindElement(By.LinkText("Services"));
        private ReadOnlyCollection<IWebElement> TableRows => _driver.FindElements(By.XPath("//table[@class='services']//tr"));

        public void NavigateTo() => ServicesLink.Click();

        public bool IsServicesTableDisplayed()
        {
            return TableRows.Count > 0;
        }
    }
}
