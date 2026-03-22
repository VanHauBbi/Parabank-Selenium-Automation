using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Pages
{
    public class ProfilePage_Hai
    {
        IWebDriver driver;

        public ProfilePage_Hai(IWebDriver driver)
        {
            this.driver = driver;
        }

        By updateProfileLink = By.LinkText("Update Contact Info");
        By address = By.Name("customer.address.street");
        By saveBtn = By.XPath("//input[@value='Update Profile']");
        By phone = By.Name("customer.phoneNumber");
        public void OpenProfile()
        {
            driver.FindElement(updateProfileLink).Click();
        }

        public void UpdateAddress(string newAddress)
        {
            driver.FindElement(address).Clear();
            driver.FindElement(address).SendKeys(newAddress);
        }

        public void UpdatePhone(string newPhone)
        {
            var field = driver.FindElement(phone);

            field.Clear();
            field.SendKeys(newPhone);
        }

        public void Save()
        {
            driver.FindElement(saveBtn).Click();
        }
    }
}
