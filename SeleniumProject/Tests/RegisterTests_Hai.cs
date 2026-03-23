using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumProject.Pages;
using SeleniumProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SeleniumProject.Tests
{
    public class RegisterTests_Hai
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
            driver?.Dispose();
        }

        [Test]
        public void TC_AUTH_01_OpenRegister()
        {
            RegisterPage_Hai register = new RegisterPage_Hai(driver);

            register.OpenRegister();

            Thread.Sleep(3000);

            Console.WriteLine(driver.Url);

            Assert.IsTrue(driver.Url.Contains("register"));
        }

        [Test]
        public void TC_AUTH_02_RegisterSuccess()
        {
            RegisterPage_Hai register = new RegisterPage_Hai(driver);

            register.OpenRegister();

            Thread.Sleep(2000);

            string username = "user" + DateTime.Now.Ticks;

            register.RegisterFull(
                "Nguyen",
                "Thanh Hai",
                "113A",
                "Ho Chi Minh",
                "Quan 8",
                "73000",
                "0967806364",
                "079205003229",
                "Haideptrai1",
                "123456789",
                "123456789"
            );

            Thread.Sleep(3000);

            Console.WriteLine(driver.PageSource);

            Assert.IsTrue(driver.PageSource.Contains("Your account was created"));
        }

        [Test]
        public void TC_AUTH_08_UsernameExists()
        {
            RegisterPage_Hai register = new RegisterPage_Hai(driver);

            register.OpenRegister();

            register.RegisterFull(
                "Nguyen",
                "Thanh Hai",
                "113A",
                "Ho Chi Minh",
                "Quan 8",
                "73000",
                "0967806364",
                "079205003229",
                "Haideptrai",   // ❗ đã tồn tại
                "123456789",
                "123456789"
            );

            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("This username already exists."));
        }
    }
}
