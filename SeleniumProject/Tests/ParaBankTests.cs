using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class ParaBankTests
    {
        private IWebDriver driver;

        // SETUP
        [SetUp]
        public void Setup()
        {
            driver = DriverFactory_D.InitDriver();

            Thread.Sleep(2000);

            // LOGIN
            driver.FindElement(By.Name("username")).SendKeys("john");
            driver.FindElement(By.Name("password")).SendKeys("demo");
            driver.FindElement(By.XPath("//input[@value='Log In']")).Click();

            Thread.Sleep(3000);
        }

        // TC01 - Open Checking
        [Test]
        public void TC01_Open_Checking_D()
        {
            driver.FindElement(By.LinkText("Open New Account")).Click();
            Thread.Sleep(2000);

            var type = new SelectElement(driver.FindElement(By.Id("type")));
            type.SelectByText("CHECKING");

            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//input[@value='Open New Account']")).Click();
            Thread.Sleep(3000);

            Assert.IsTrue(driver.PageSource.Contains("Account Opened"));
        }

        // TC02 - Open Savings
        [Test]
        public void TC02_Open_Savings_D()
        {
            driver.FindElement(By.LinkText("Open New Account")).Click();
            Thread.Sleep(2000);

            var type = new SelectElement(driver.FindElement(By.Id("type")));
            type.SelectByText("SAVINGS");

            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//input[@value='Open New Account']")).Click();
            Thread.Sleep(3000);

            Assert.IsTrue(driver.PageSource.Contains("Account Opened"));
        }

        // TC03 - Overview
        [Test]
        public void TC03_Accounts_Overview_D()
        {
            driver.FindElement(By.LinkText("Accounts Overview")).Click();
            Thread.Sleep(3000);

            Assert.IsTrue(driver.PageSource.Contains("Account"));
        }

        // TC04 - Admin Page
        [Test]
        public void TC04_Admin_Page_D()
        {
            driver.FindElement(By.LinkText("Admin Page")).Click();
            Thread.Sleep(3000);

            Assert.IsTrue(driver.PageSource.Contains("Administration"));
        }

        // TC05 - Savings hiển thị (có thể FAIL - do hệ thống không có text này)
        [Test]
        public void TC05_Check_Savings_Display_D()
        {
            driver.FindElement(By.LinkText("Open New Account")).Click();
            Thread.Sleep(2000);

            var type = new SelectElement(driver.FindElement(By.Id("type")));
            type.SelectByText("SAVINGS");

            driver.FindElement(By.XPath("//input[@value='Open New Account']")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.LinkText("Accounts Overview")).Click();
            Thread.Sleep(3000);

            Assert.IsTrue(driver.PageSource.Contains("Savings"));
        }

        // TC10 - Overview hiển thị
        [Test]
        public void TC10_Accounts_Overview_Display_D()
        {
            driver.FindElement(By.LinkText("Accounts Overview")).Click();
            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("Accounts Overview"));
        }

        // TC11 - Thông tin tài khoản
        [Test]
        public void TC11_Check_Account_Info_D()
        {
            driver.FindElement(By.LinkText("Accounts Overview")).Click();
            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("Balance"));
            Assert.IsTrue(driver.PageSource.Contains("Available"));
        }

        // TC13 - Total Balance
        [Test]
        public void TC13_Check_Total_Balance_D()
        {
            driver.FindElement(By.LinkText("Accounts Overview")).Click();
            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("Total"));
        }

        // TC16 - Admin Page
        [Test]
        public void TC16_Open_Admin_Page_D()
        {
            driver.FindElement(By.LinkText("Admin Page")).Click();
            Thread.Sleep(2000);

            Assert.IsTrue(driver.PageSource.Contains("Administration"));
        }

        // TC19 - Initialize DB
        [Test]
        public void TC19_Initialize_Database_D()
        {
            driver.FindElement(By.LinkText("Admin Page")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//button[contains(text(),'Initialize')]")).Click();
            Thread.Sleep(3000);

            Assert.Pass();
        }

        // TEARDOWN
        [TearDown]
        public void TearDown()
        {
            try
            {
                // Nếu test FAIL → chụp màn hình
                if (TestContext.CurrentContext.Result.Outcome.Status
                    == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    // Chụp ảnh
                    ITakesScreenshot ts = (ITakesScreenshot)driver;
                    Screenshot screenshot = ts.GetScreenshot();

                    // Đường dẫn folder
                    string folderPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "Screenshots"
                    );

                    // Nếu chưa có folder thì tạo
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Tên file (có timestamp)
                    string fileName = TestContext.CurrentContext.Test.Name
                                    + "_"
                                    + DateTime.Now.ToString("yyyyMMdd_HHmmss")
                                    + ".png";

                    string fullPath = Path.Combine(folderPath, fileName);

                    // Lưu ảnh
                    screenshot.SaveAsFile(fullPath);
                }
            }
            catch
            {
                // nếu lỗi thì bỏ qua để không crash test
            }
            finally
            {
                if (driver != null)
                {
                    Thread.Sleep(2000);
                    driver.Quit();
                    driver.Dispose();
                    driver = null;
                }
            }
        }
    }
}
