using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumProject.Pages;
using SeleniumProject.Utilities;

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

        [TearDown]
        public void TearDown()
        {
            _driverFactory.CloseDriver();
        }
    }
}