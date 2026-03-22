using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumProject.Pages;
using SeleniumProject.Utilities;

namespace SeleniumProject.Tests
{
    public class ProfileTests_Hai
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
        public void TC_AUTH_26_UpdateAddress()
        {
            LoginPage_Hai login = new LoginPage_Hai(driver);
            ProfilePage_Hai profile = new ProfilePage_Hai(driver);

            login.Login("Haideptrai", "123456789");

            Thread.Sleep(3000);

            // ❗ QUAN TRỌNG: phải vào trang profile
            profile.OpenProfile();

            Thread.Sleep(2000);

            profile.UpdateAddress("New Address 123");

            profile.Save();

            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("Profile Updated"));
        }

        [Test]
        public void TC_AUTH_30_InvalidPhone()
        {
            LoginPage_Hai login = new LoginPage_Hai(driver);
            ProfilePage_Hai profile = new ProfilePage_Hai(driver);

            login.Login("Haideptrai", "123456789");

            Thread.Sleep(2000);

            profile.OpenProfile();
            profile.UpdatePhone("abc123");
            profile.Save();

            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("error"));
        }
    }
}
