using System;
using NUnit.Framework;
using SeleniumProject.Pages;
using SeleniumProject.Utilities;

namespace SeleniumProject.Tests
{
    [TestFixture]
    public class FindTransactionsTests
    {
        private DriverFactory _driverFactory;
        private LoginPage _loginPage;
        private FindTransactionsPage _findTransactionsPage;

        [SetUp]
        public void SetUp()
        {
            _driverFactory = new DriverFactory();
            _driverFactory.InitDriver();
            _driverFactory.Driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");

            _loginPage = new LoginPage(_driverFactory.Driver);
            _findTransactionsPage = new FindTransactionsPage(_driverFactory.Driver);

            _loginPage.LoginToParabank("john", "demo");
        }

        [Test]
        public void TC_FIN_01_FindById()
        {
            try
            {
                _findTransactionsPage.GoToFindTransactionsPage();

                string transactionId = "14169";
                _findTransactionsPage.SearchById(transactionId);

                bool isFound = _findTransactionsPage.IsTransactionTableDisplayed();

                if (isFound)
                {
                    Assert.IsTrue(isFound);
                    ExcelHelper.WriteResult(8, 14, "PASS", 13, "Hệ thống chuyển hướng và hiển thị chi tiết giao dịch.");
                }
                else
                {
                    string error = _findTransactionsPage.GetErrorMessage();
                    ExcelHelper.WriteResult(8, 14, "FAIL", 13, $"Không tìm thấy giao dịch. Phản hồi: {error}");
                    Assert.Fail("Bảng kết quả không hiển thị với ID hợp lệ.");
                }
            }
            catch (Exception ex)
            {
                ExcelHelper.WriteResult(8, 14, "FAIL", 13, ex.Message);
                throw;
            }
        }

        [Test]
        public void TC_FIN_03_FindByDateRange()
        {
            try
            {
                _findTransactionsPage.GoToFindTransactionsPage();

                _findTransactionsPage.SearchByDateRange("11-01-2023", "11-30-2023");

                bool isFound = _findTransactionsPage.IsTransactionTableDisplayed();

                Assert.IsTrue(isFound, "Không tìm thấy giao dịch trong khoảng thời gian chỉ định.");
                ExcelHelper.WriteResult(10, 14, "PASS", 13, "Hiển thị đúng danh sách giao dịch.");
            }
            catch (Exception ex)
            {
                ExcelHelper.WriteResult(10, 14, "FAIL", 13, ex.Message);
                throw;
            }
        }

        [Test]
        public void TC_FIN_06_FindById_Blank()
        {
            try
            {
                _findTransactionsPage.GoToFindTransactionsPage();

                _findTransactionsPage.SearchById("");

                string errorMessage = _findTransactionsPage.GetErrorMessage();

                Assert.That(errorMessage, Is.Not.Empty, "Lỗi UI: Hệ thống không hiển thị thông báo khi để trống ID.");
                ExcelHelper.WriteResult(24, 14, "PASS", 13, errorMessage);
            }
            catch (Exception ex)
            {
                ExcelHelper.WriteResult(24, 14, "FAIL", 13, ex.Message);
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