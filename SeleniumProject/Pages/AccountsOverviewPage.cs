using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace SeleniumProject.Pages
{
    public class AccountsOverviewPage
    {
        private IWebDriver _driver;

        public AccountsOverviewPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private By AccountsOverviewMenu = By.LinkText("Accounts Overview");

        private By FirstAccountBalance = By.XPath("//table[@id='accountTable']/tbody/tr[1]/td[2]");

        public void GoToAccountsOverview()
        {
            _driver.FindElement(AccountsOverviewMenu).Click();
            System.Threading.Thread.Sleep(1000); // Chờ bảng tải xong
        }

        public double GetFirstAccountBalance()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElements(FirstAccountBalance).Count > 0);

            string balanceText = _driver.FindElement(FirstAccountBalance).Text;

            balanceText = balanceText.Replace("$", "").Replace(",", "").Trim();

            return double.Parse(balanceText, CultureInfo.InvariantCulture);
        }
    }
}