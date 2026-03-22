using OpenQA.Selenium;
using ParaBankAutomation.Utilities;

namespace ParaBankAutomation.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage()
        {
            _driver = DriverFactory.GetDriver();
        }

        // Định nghĩa các Elements (Selectors)
        private IWebElement UsernameInput => _driver.FindElement(By.Name("username"));
        private IWebElement PasswordInput => _driver.FindElement(By.Name("password"));
        private IWebElement LoginButton => _driver.FindElement(By.XPath("//input[@value='Log In']"));
        private IWebElement ErrorMessage => _driver.FindElement(By.ClassName("error"));

       
        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");
        }

        public void Login(string username, string password)
        {
            UsernameInput.Clear();
            UsernameInput.SendKeys(username);
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
            LoginButton.Click();
        }

        public string GetErrorMessage()
        {
            return ErrorMessage.Text;
        }
    }
}