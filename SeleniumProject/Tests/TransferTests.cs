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
    [TestFixture]
    public class TransferTests
    {
        private DriverFactory _driverFactory;
        private LoginPage _loginPage;
        private TransferFundsPage _transferPage;

        [SetUp]
        public void SetUp()
        {
            _driverFactory = new DriverFactory();
            _driverFactory.InitDriver();
            _driverFactory.Driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");

            _loginPage = new LoginPage(_driverFactory.Driver);
            _transferPage = new TransferFundsPage(_driverFactory.Driver);

            _loginPage.LoginToParabank("john", "demo");
        }

        [Test]
        public void TC_TRA_01_ValidTransfer()
        {
            try
            {
                _transferPage.GoToTransferPage();
                _transferPage.EnterAmount("100");
                _transferPage.SelectFromAccountByIndex(0);
                _transferPage.SelectToAccountByIndex(0);

                _transferPage.ClickTransferButton();

                string actualMessage = _transferPage.GetResultMessage();

                Assert.That(actualMessage, Is.EqualTo("Transfer Complete!"));

                ExcelHelper.WriteResult(2, 14, "PASS", 13, actualMessage);
            }
            catch (Exception)
            {
                ExcelHelper.WriteResult(2, 14, "FAIL", 13, "Lỗi hoặc không hiện thông báo thành công");
                throw;
            }
        }

        [Test]
        public void TC_TRA_02_EmptyAmount()
        {
            try
            {
                _transferPage.GoToTransferPage();

                _transferPage.EnterAmount("");
                _transferPage.SelectFromAccountByIndex(0);
                _transferPage.SelectToAccountByIndex(1);
                _transferPage.ClickTransferButton();

                string actualError = _transferPage.GetAmountErrorText();

                Assert.That(actualError, Is.Not.Empty, "Hệ thống không hiển thị báo lỗi khi để trống Amount.");

                ExcelHelper.WriteResult(3, 14, "PASS", 13, actualError);
            }
            catch (Exception ex)
            {
                ExcelHelper.WriteResult(3, 14, "FAIL", 13, ex.Message);
                throw;
            }
        }

        [Test]
        public void TC_TRA_04_SameAccount()
        {
            try
            {
                _transferPage.GoToTransferPage();
                _transferPage.EnterAmount("50");
                _transferPage.SelectFromAccountByIndex(0);
                _transferPage.SelectToAccountByIndex(0);
                _transferPage.ClickTransferButton();

                bool isBlocked = false;
                try
                {
                    _transferPage.GetResultMessage();
                }
                catch (WebDriverTimeoutException)
                {
                    isBlocked = true;
                }

                ExcelHelper.TakeScreenshot(_driverFactory.Driver, "TC_TRA_04_SameAccount");

                Assert.IsTrue(isBlocked, "Lỗi: Hệ thống cho phép chuyển tiền cùng một tài khoản.");

                ExcelHelper.WriteResult(12, 14, "Passed", 13, "Chặn thành công giao dịch trùng tài khoản");
            }
            catch (Exception ex)
            {
                ExcelHelper.TakeScreenshot(_driverFactory.Driver, "TC_TRA_04_SameAccount_FAIL");

                ExcelHelper.WriteResult(12, 14, "Failed", 13, $"Lỗi logic xử lý tài khoản trùng: {ex.Message}");
                throw;
            }
        }

        [Test]
        public void TC_TRA_05_Overdraft()
        {
            string testCaseId = "TC_TRA_05";
            int excelRow = 14;

            try
            {
                _transferPage.GoToTransferPage();
                System.Threading.Thread.Sleep(2000);

                _transferPage.EnterAmount("999999");
                _transferPage.SelectFromAccountByIndex(0);

                _transferPage.SelectToAccountByIndex(1);

                _transferPage.ClickTransferButton();

                string actualMessage = _transferPage.GetResultMessage();

                Assert.That(actualMessage, Is.Not.EqualTo("Transfer Complete!"),
                    "LỖI NGHIỆP VỤ (BUG): Hệ thống đã cho phép chuyển tiền vượt quá số dư thực tế (Overdraft).");

                ExcelHelper.TakeScreenshot(_driverFactory.Driver, testCaseId);
                ExcelHelper.WriteResult(excelRow, 14, "PASS", 13, "Hệ thống đã chặn thành công giao dịch vượt hạn mức.");
            }
            catch (AssertionException ex)
            {
                ExcelHelper.TakeScreenshot(_driverFactory.Driver, $"{testCaseId}_BUG_Overdraft");
                ExcelHelper.WriteResult(excelRow, 14, "FAIL", 13, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                ExcelHelper.TakeScreenshot(_driverFactory.Driver, $"{testCaseId}_FAIL");
                ExcelHelper.WriteResult(excelRow, 14, "FAIL", 13, $"Lỗi kỹ thuật: {ex.Message}");
                throw;
            }
        }

        [TearDown]
        public void TearDown()
        {
            _driverFactory.CloseDriver();
        }
    }
}