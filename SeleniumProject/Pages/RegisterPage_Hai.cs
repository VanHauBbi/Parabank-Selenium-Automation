using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Pages
{
    internal class RegisterPage_Hai
    {
        IWebDriver driver;

        public RegisterPage_Hai(IWebDriver driver)
        {
            this.driver = driver;
        }

        By registerLink = By.LinkText("Register");

        By firstName = By.Name("customer.firstName");
        By lastName = By.Name("customer.lastName");
        By address = By.Name("customer.address.street");
        By city = By.Name("customer.address.city");
        By state = By.Name("customer.address.state");
        By zipCode = By.Name("customer.address.zipCode");
        By phone = By.Name("customer.phoneNumber");
        By ssn = By.Name("customer.ssn");

        By username = By.Name("customer.username");
        By password = By.Name("customer.password");
        By confirmPassword = By.Name("repeatedPassword");

        By registerBtn = By.XPath("//input[@value='Register']");

        public void OpenRegister()
        {
            driver.FindElement(registerLink).Click();
        }

        public void RegisterFull(
            string fName,
            string lName,
            string addr,
            string cityName,
            string stateName,
            string zip,
            string phoneNum,
            string ssnNum,
            string user,
            string pass,
            string confirmPass)
        {
            driver.FindElement(firstName).SendKeys(fName);
            driver.FindElement(lastName).SendKeys(lName);
            driver.FindElement(address).SendKeys(addr);
            driver.FindElement(city).SendKeys(cityName);
            driver.FindElement(state).SendKeys(stateName);
            driver.FindElement(zipCode).SendKeys(zip);
            driver.FindElement(phone).SendKeys(phoneNum);
            driver.FindElement(ssn).SendKeys(ssnNum);

            driver.FindElement(username).SendKeys(user);
            driver.FindElement(password).SendKeys(pass);
            driver.FindElement(confirmPassword).SendKeys(confirmPass);

            driver.FindElement(registerBtn).Click();
        }
    }
}
