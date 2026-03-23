using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumProject.Pages
{
    public class BillPayPage
    {
        private IWebDriver _driver;

        public BillPayPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private By BillPayMenuLink = By.LinkText("Bill Pay");
        private By PayeeNameInput = By.Name("payee.name");
        private By AddressInput = By.Name("payee.address.street");
        private By CityInput = By.Name("payee.address.city");
        private By StateInput = By.Name("payee.address.state");
        private By ZipCodeInput = By.Name("payee.address.zipCode");
        private By PhoneInput = By.Name("payee.phoneNumber");
        private By AccountInput = By.Name("payee.accountNumber");
        private By VerifyAccountInput = By.Name("verifyAccount");
        private By AmountInput = By.Name("amount");
        private By SendPaymentButton = By.XPath("//input[@value='Send Payment']");

        public void GoToBillPayPage()
        {
            _driver.FindElement(BillPayMenuLink).Click();
            System.Threading.Thread.Sleep(1000);
        }

        public void FillPaymentForm(string name, string address, string city, string state, string zip, string phone, string account, string verifyAcc, string amount)
        {
            _driver.FindElement(PayeeNameInput).SendKeys(name);
            _driver.FindElement(AddressInput).SendKeys(address);
            _driver.FindElement(CityInput).SendKeys(city);
            _driver.FindElement(StateInput).SendKeys(state);
            _driver.FindElement(ZipCodeInput).SendKeys(zip);
            _driver.FindElement(PhoneInput).SendKeys(phone);
            _driver.FindElement(AccountInput).SendKeys(account);
            _driver.FindElement(VerifyAccountInput).SendKeys(verifyAcc);
            _driver.FindElement(AmountInput).SendKeys(amount);
        }

        public void ClickSendPayment()
        {
            _driver.FindElement(SendPaymentButton).Click();
        }

        public string GetSuccessMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            By successMessageLocator = By.XPath("//h1[contains(text(), 'Complete')]");

            wait.Until(d =>
            {
                var elements = d.FindElements(successMessageLocator);
                return elements.Count > 0 && elements[0].Displayed;
            });

            return _driver.FindElement(successMessageLocator).Text.Trim();
        }

        public string GetAccountMismatchError()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            wait.Until(d =>
            {
                try
                {
                    var errorElements = d.FindElements(By.ClassName("error"));
                    foreach (var element in errorElements)
                    {
                        if (element.Displayed && !string.IsNullOrWhiteSpace(element.Text))
                        {
                            return true;
                        }
                    }

                    if (d.PageSource.Contains("An internal error has occurred"))
                    {
                        return true;
                    }

                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });

            if (_driver.PageSource.Contains("An internal error has occurred"))
            {
                return "Server Parabank Crash: An internal error has occurred.";
            }

            var finalErrors = _driver.FindElements(By.ClassName("error"));
            foreach (var err in finalErrors)
            {
                if (err.Displayed && !string.IsNullOrWhiteSpace(err.Text))
                {
                    return err.Text.Trim();
                }
            }

            return string.Empty;
        }
    }
}