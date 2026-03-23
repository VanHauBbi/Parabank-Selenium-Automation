using System;
using NUnit.Framework;
using SeleniumProject.Pages;
using SeleniumProject.Utilities;

namespace SeleniumProject.Tests
{
    [TestFixture]
    public class BalanceTests
    {
        private DriverFactory _driverFactory;
        private LoginPage _loginPage;
        private BillPayPage _billPayPage;
        private AccountsOverviewPage _accountsPage;

        [SetUp]
        public void SetUp()
        {
            _driverFactory = new DriverFactory();
            _driverFactory.InitDriver();
            _driverFactory.Driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");

            _loginPage = new LoginPage(_driverFactory.Driver);
            _billPayPage = new BillPayPage(_driverFactory.Driver);
            _accountsPage = new AccountsOverviewPage(_driverFactory.Driver);

            _loginPage.LoginToParabank("john", "demo");
        }

        [Test]
        public void TC_BAL_01_CheckBalanceDeduction()
        {
            try
            {
                double transferAmount = 50.00;

                // 1. Vào trang Tổng quan, ghi nhận số dư ban đầu
                _accountsPage.GoToAccountsOverview();
                double initialBalance = _accountsPage.GetFirstAccountBalance();

                // 2. Sang trang Bill Pay, thực hiện chuyển đi 50$
                _billPayPage.GoToBillPayPage();
                _billPayPage.FillPaymentForm(
                    "Đại học HUFLIT", "123 Sư Vạn Hạnh", "HCM", "SG", "70000", "0912345678",
                    "112233", "112233", transferAmount.ToString()
                );
                _billPayPage.ClickSendPayment();
                _billPayPage.GetSuccessMessage(); // Chờ giao dịch hoàn tất

                // 3. Quay lại trang Tổng quan, ghi nhận số dư mới
                _accountsPage.GoToAccountsOverview();
                double newBalance = _accountsPage.GetFirstAccountBalance();

                // 4. KIỂM CHỨNG TOÁN HỌC (ASSERTION)
                double expectedNewBalance = initialBalance - transferAmount;

                Assert.That(newBalance, Is.EqualTo(expectedNewBalance),
                    $"Lỗi Toán học: Số dư cũ {initialBalance} trừ đi {transferAmount} phải bằng {expectedNewBalance}, nhưng web lại hiển thị {newBalance}");

                // Ghi PASS vào dòng số 6 trong Excel (Theo hình ảnh bạn cung cấp, TC_BAL_01 nằm ở dòng 6)
                ExcelHelper.WriteResult(6, 14, "PASS", 13, $"Đã trừ tiền chính xác. Số dư cũ: ${initialBalance} -> Số dư mới: ${newBalance}");
            }
            catch (Exception ex)
            {
                ExcelHelper.WriteResult(6, 14, "FAIL", 13, ex.Message);
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