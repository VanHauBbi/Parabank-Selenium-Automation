using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ParaBankAutomation.Utilities;

namespace ParaBankAutomation.Pages
{
    public class LoanPage
    {
        private readonly IWebDriver _driver;

        public LoanPage()
        {
            _driver = DriverFactory.GetDriver();
        }

        // --- Định nghĩa Elements ---
        private IWebElement RequestLoanLink => _driver.FindElement(By.LinkText("Request Loan"));
        private IWebElement LoanAmountInput => _driver.FindElement(By.Id("amount"));
        private IWebElement DownPaymentInput => _driver.FindElement(By.Id("downPayment"));
        private IWebElement FromAccountDropdown => _driver.FindElement(By.Id("fromAccountId"));
        private IWebElement ApplyNowButton => _driver.FindElement(By.XPath("//input[@value='Apply Now']"));

        // Elements sau khi Submit
        private IWebElement ResultStatus => _driver.FindElement(By.Id("loanStatus"));
        private IWebElement ResultMessage => _driver.FindElement(By.ClassName("result-message")); // Dùng cho báo lỗi logic
        private IWebElement ValidationError => _driver.FindElement(By.Id("amount.errors")); // Cho validation trống

        // --- Các hành động ---
        public void NavigateToLoanPage()
        {
            RequestLoanLink.Click();
        }

        public void FillLoanRequest(string amount, string downPayment, string fromAccountIndex = "0")
        {
            // Điền thông tin
            LoanAmountInput.Clear();
            LoanAmountInput.SendKeys(amount);
            DownPaymentInput.Clear();
            DownPaymentInput.SendKeys(downPayment);

            // Chọn tài khoản nguồn
            var selectElement = new SelectElement(FromAccountDropdown);
            selectElement.SelectByIndex(int.Parse(fromAccountIndex)); // Chọn theo index (0 là tk đầu tiên)
        }

        public void ClickApplyNow()
        {
            ApplyNowButton.Click();
        }

        // Method tiện ích thực hiện trọn gói
        public void RequestLoan(string amount, string downPayment, string fromAccountIndex = "0")
        {
            NavigateToLoanPage();

            
            System.Threading.Thread.Sleep(2000);
            FillLoanRequest(amount, downPayment, fromAccountIndex);         
            System.Threading.Thread.Sleep(1000);
            ClickApplyNow();
            System.Threading.Thread.Sleep(2000);
        }

        // --- Get thông tin kết quả ---
        public string GetLoanStatus()
        {
           
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(d => ResultStatus.Displayed);
            return ResultStatus.Text;
        }

        public string GetResultStatus()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                // Đợi cho đến khi ID loanStatus hiển thị trên màn hình
                IWebElement status = wait.Until(d => d.FindElement(By.Id("loanStatus")));
                return status.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return "Timeout: Không tìm thấy kết quả duyệt vay.";
            }
        }

        public string GetValidationError()
        {
            return ValidationError.Text;
        }
    }
}