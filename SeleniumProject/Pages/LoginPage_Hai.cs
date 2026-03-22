using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Pages
{
    public class LoginPage_Hai
    {
        IWebDriver driver;

        public LoginPage_Hai(IWebDriver driver)
        {
            this.driver = driver;
        }

        By username = By.Name("username");
        By password = By.Name("password");
        By loginBtn = By.XPath("//input[@value='Log In']");
        By logoutLink = By.LinkText("Log Out");

        public void Login(string user, string pass)
        {
            driver.FindElement(username).Clear();
            driver.FindElement(username).SendKeys(user);

            driver.FindElement(password).Clear();
            driver.FindElement(password).SendKeys(pass);

            driver.FindElement(loginBtn).Click();
        }

        public void Logout()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var logoutBtn = wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.LinkText("Log Out"))
            );

            logoutBtn.Click();
        }
    }
}
