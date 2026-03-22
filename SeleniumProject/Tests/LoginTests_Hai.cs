using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumProject.Pages;
using SeleniumProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SeleniumProject.Tests
{
    [NonParallelizable]
    public class LoginTests_Hai
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
        public void TC_AUTH_11_LoginSuccess()
        {
            LoginPage_Hai login = new LoginPage_Hai(driver);

            login.Login("Haideptrai", "123456789");

            Thread.Sleep(3000);

            Console.WriteLine(driver.Url);

            Assert.IsTrue(driver.Url.Contains("overview"));
        }

        [Test]
        public void TC_AUTH_13_WrongPassword()
        {
            LoginPage_Hai login = new LoginPage_Hai(driver);

            login.Login("Haideptrai", "wrongpass");

            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("Error"));
        }

        [Test]
        public void TC_AUTH_17_EmptyLogin()
        {
            LoginPage_Hai login = new LoginPage_Hai(driver);

            login.Login("", "");

            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("Error"));
        }
    }
}
