using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumProject.Pages
{
    public class TransferFundsPage
    {
        private IWebDriver _driver;
        private By AmountError = By.Id("amount.errors");

        public TransferFundsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private By TransferMenuLink = By.LinkText("Transfer Funds");
        private By AmountInput = By.Id("amount");
        private By FromAccountDropdown = By.Id("fromAccountId");
        private By ToAccountDropdown = By.Id("toAccountId");
        private By TransferButton = By.XPath("//input[@value='Transfer']");
        private By SuccessMessage = By.XPath("//h1[@class='title']");

        public void GoToTransferPage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            wait.Until(d =>
            {
                try
                {
                    IWebElement menuLink = d.FindElement(TransferMenuLink);
                    menuLink.Click();
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (ElementClickInterceptedException)
                {
                    return false;
                }
            });

            System.Threading.Thread.Sleep(1000);
        }

        public void EnterAmount(string amount)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(AmountInput).Displayed);
            _driver.FindElement(AmountInput).SendKeys(amount);
        }

        public void SelectFromAccountByIndex(int index)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            wait.Until(d => new SelectElement(d.FindElement(FromAccountDropdown)).Options.Count > 0);

            SelectElement select = new SelectElement(_driver.FindElement(FromAccountDropdown));
            select.SelectByIndex(index);
        }

        public void SelectToAccountByIndex(int index)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            IWebElement toAccountDrop = wait.Until(d => d.FindElement(By.Id("toAccountId")));
            SelectElement select = new SelectElement(toAccountDrop);

            wait.Until(d => new SelectElement(d.FindElement(By.Id("toAccountId"))).Options.Count > 0);

            int availableOptions = select.Options.Count;

            if (index >= availableOptions)
            {
                Console.WriteLine($"Cảnh báo: Không có index {index}, chọn index {availableOptions - 1} thay thế.");
                select.SelectByIndex(availableOptions - 1);
            }
            else
            {
                select.SelectByIndex(index);
            }
        }

        public void ClickTransferButton()
        {
            System.Threading.Thread.Sleep(1500);
            _driver.FindElement(TransferButton).Click();
        }

        public string GetResultMessage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            By trueSuccessMessage = By.XPath("//h1[contains(text(), 'Complete')]");

            wait.Until(d =>
            {
                var elements = d.FindElements(trueSuccessMessage);
                if (elements.Count > 0)
                {
                    var element = elements[0];
                    return element.Displayed && !string.IsNullOrWhiteSpace(element.Text);
                }
                return false;
            });

            return _driver.FindElement(trueSuccessMessage).Text.Trim();
        }

        public string GetAmountErrorText()
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
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });

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