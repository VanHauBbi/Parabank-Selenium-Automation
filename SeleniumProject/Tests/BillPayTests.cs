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
            // 1. Khởi tạo trình duyệt và truy cập Parabank
            _driverFactory = new DriverFactory();
            _driverFactory.InitDriver();
            _driverFactory.Driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");

            // 2. Khởi tạo các trang POM
            _loginPage = new LoginPage(_driverFactory.Driver);
            _billPayPage = new BillPayPage(_driverFactory.Driver);

            // 3. Đăng nhập (Bắt buộc phải có để vào được bên trong)
            _loginPage.LoginToParabank("john", "demo");
        }

        [Test]
        public void TC_BIL_01_ValidPayment()
        {
            try
            {
                _billPayPage.GoToBillPayPage();

                // Điền form hợp lệ (Account và Verify Account giống hệt nhau)
                _billPayPage.FillPaymentForm(
                    "Điện lực EVN",
                    "123 Điện Biên Phủ",
                    "TP. Hồ Chí Minh",
                    "SG",
                    "700000",
                    "0901234567",
                    "998877",   // Account
                    "998877",   // Verify Account
                    "150"       // Amount
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
                    "112233",   // Account đúng
                    "999999",   // Verify Account SAI
                    "50"
                );

                _billPayPage.ClickSendPayment();

                string actualError = _billPayPage.GetAccountMismatchError();

                Assert.That(actualError, Is.EqualTo("The account numbers do not match."), "Lỗi GUI: Hệ thống không báo lỗi khi số tài khoản xác nhận bị sai.");

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