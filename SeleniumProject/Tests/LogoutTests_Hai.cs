using OpenQA.Selenium;
using SeleniumProject.Pages;
using SeleniumProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SeleniumProject.Tests
{
    public class LogoutTests_Hai
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory_Hai.InitDriver();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void TC_AUTH_21_Logout()
        {
            LoginPage_Hai login = new LoginPage_Hai(driver);

            login.Login("Haideptrai", "123456789");

            Thread.Sleep(3000);

            // đảm bảo login OK
            Assert.IsTrue(driver.PageSource.Contains("Accounts Overview"));

            login.Logout();

            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("Customer Login") || driver.Url.Contains("index"));
        }
    }
}
