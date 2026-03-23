using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumProject.Pages
{
    public class FindTransactionsPage
    {
        private IWebDriver _driver;

        public FindTransactionsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // --- LOCATORS ĐÃ CHUẨN HÓA THEO MÃ NGUỒN MỚI ---
        private By FindTransactionsMenu = By.LinkText("Find Transactions");

        // Form inputs
        private By TransactionIdInput = By.Id("transactionId");
        private By FindByIdBtn = By.Id("findById");

        private By FromDateInput = By.Id("fromDate");
        private By ToDateInput = By.Id("toDate");
        private By FindByDateRangeBtn = By.Id("findByDateRange");

        // Kết quả và Lỗi
        private By TransactionResultsTable = By.Id("transactionTable");
        private By ErrorContainer = By.Id("errorContainer");
        private By FormContainer = By.Id("formContainer");

        // --- ACTIONS ---
        public void GoToFindTransactionsPage()
        {
            _driver.FindElement(FindTransactionsMenu).Click();

            // Chờ form container xuất hiện
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(FormContainer).Displayed);
        }

        public void SearchById(string id)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(TransactionIdInput).Displayed);

            _driver.FindElement(TransactionIdInput).Clear();
            _driver.FindElement(TransactionIdInput).SendKeys(id);
            _driver.FindElement(FindByIdBtn).Click();
        }

        public void SearchByDateRange(string fromDate, string toDate)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(FromDateInput).Displayed);

            _driver.FindElement(FromDateInput).Clear();
            _driver.FindElement(FromDateInput).SendKeys(fromDate);

            _driver.FindElement(ToDateInput).Clear();
            _driver.FindElement(ToDateInput).SendKeys(toDate);

            _driver.FindElement(FindByDateRangeBtn).Click();
        }

        public bool IsTransactionTableDisplayed()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                // jQuery thay đổi display từ none sang block
                wait.Until(d => d.FindElement(TransactionResultsTable).Displayed);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public string GetErrorMessage()
        {
            try
            {
                // Kiểm tra xem errorContainer có đang hiển thị không (trường hợp API trả về lỗi 500)
                if (_driver.FindElement(ErrorContainer).Displayed)
                {
                    return _driver.FindElement(By.CssSelector("#errorContainer .error")).Text.Trim();
                }

                // Kiểm tra các lỗi validation trên form
                var errors = _driver.FindElements(By.CssSelector("#formContainer span.error"));
                foreach (var err in errors)
                {
                    if (err.Displayed && !string.IsNullOrWhiteSpace(err.Text))
                        return err.Text.Trim();
                }

                return string.Empty;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }
    }
}