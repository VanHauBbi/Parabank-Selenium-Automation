using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Pages
{
    public class OpenAccountPage
    {
        IWebDriver driver;

        public OpenAccountPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement linkOpenAccount => driver.FindElement(By.LinkText("Open New Account"));
        IWebElement accountType => driver.FindElement(By.Id("type"));
        IWebElement btnOpen => driver.FindElement(By.XPath("//input[@value='Open New Account']"));

        public void OpenCheckingAccount()
        {
            linkOpenAccount.Click();

            SelectElement type = new SelectElement(accountType);
            type.SelectByText("CHECKING");

            btnOpen.Click();
        }

        public void OpenSavingsAccount()
        {
            linkOpenAccount.Click();

            SelectElement type = new SelectElement(accountType);
            type.SelectByText("SAVINGS");

            btnOpen.Click();
        }
    }
}
