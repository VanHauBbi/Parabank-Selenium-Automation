using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Pages
{
    public class LoginPage_D
    {
        IWebDriver driver;

        public LoginPage_D(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement txtUsername => driver.FindElement(By.Name("username"));
        IWebElement txtPassword => driver.FindElement(By.Name("password"));
        IWebElement btnLogin => driver.FindElement(By.XPath("//input[@value='Log In']"));

        public void Login(string username, string password)
        {
            txtUsername.SendKeys(username);
            txtPassword.SendKeys(password);
            btnLogin.Click();
        }
    }
}
