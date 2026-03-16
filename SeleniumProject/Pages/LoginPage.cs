using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumProject.Pages
{
    public class LoginPage
    {
        private IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private By UsernameInput = By.Name("username");
        private By PasswordInput = By.Name("password");
        private By LoginButton = By.XPath("//input[@value='Log In']");

        public void LoginToParabank(string username, string password)
        {
            _driver.FindElement(UsernameInput).SendKeys(username);
            _driver.FindElement(PasswordInput).SendKeys(password);
            _driver.FindElement(LoginButton).Click();
        }
    }
}
