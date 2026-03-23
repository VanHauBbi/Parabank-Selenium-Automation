using System;
using NUnit.Framework;
using SeleniumProject.Pages;
using SeleniumProject.Utilities;

namespace SeleniumProject.Tests
{
    [TestFixture]
    public class BillPayTests
    {
        private DriverFactory _driverFactory;
        private LoginPage _loginPage;
        private BillPayPage _billPayPage;

        [SetUp]
        public void SetUp()
        {
            _driverFactory = new DriverFactory();
            _driverFactory.InitDriver();
            _driverFactory.Driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");

            _loginPage = new LoginPage(_driverFactory.Driver);
            _billPayPage = new BillPayPage(_driverFactory.Driver);

            _loginPage.LoginToParabank("john", "demo");
        }

        [Test]
        public void TC_BIL_01_ValidPayment()
        {
            try
            {
                _billPayPage.GoToBillPayPage();

                _billPayPage.FillPaymentForm(
                    "Điện lực EVN",
                    "123 Điện Biên Phủ",
                    "TP. Hồ Chí Minh",
                    "SG",
                    "700000",
                    "0901234567",
                    "998877", 
                    "998877",
                    "150"     
                );

                _billPayPage.ClickSendPayment();

                string actualMessage = _billPayPage.GetSuccessMessage();

                Assert.That(actualMessage, Is.Not.Empty, "Lỗi: Không hiển thị thông báo thanh toán thành công.");

                ExcelHelper.WriteResult(17, 14, "PASS", 13, actualMessage);
            }
            catch (Exception ex)
            {
                ExcelHelper.WriteResult(17, 14, "FAIL", 13, ex.Message);
                throw;
            }
        }

        [Test]
        public void TC_BIL_02_AccountMismatch()
        {
            try
            {
                _billPayPage.GoToBillPayPage();

                _billPayPage.FillPaymentForm(
                    "Cấp nước SAWACO",
                    "456 Lê Lợi",
                    "Đà Nẵng",
                    "DN",
                    "500000",
                    "0987654321",
                    "112233",
                    "999999",
                    "50"
                );

                _billPayPage.ClickSendPayment();

                string actualError = _billPayPage.GetAccountMismatchError();

                Assert.That(actualError.Contains("not match") || actualError.Contains("Crash"), Is.True, "Lỗi: Hệ thống phản hồi hoàn toàn sai lệch.");

                ExcelHelper.WriteResult(18, 14, "PASS", 13, actualError);
            }
            catch (Exception ex)
            {
                ExcelHelper.WriteResult(18, 14, "FAIL", 13, ex.Message);
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