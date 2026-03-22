using OpenQA.Selenium;
using ParaBankAutomation.Utilities;
using System.Collections.ObjectModel;

namespace ParaBankAutomation.Pages
{
    public class ServicesPage
    {
        private readonly IWebDriver _driver;
        public ServicesPage() => _driver = DriverFactory.GetDriver();

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