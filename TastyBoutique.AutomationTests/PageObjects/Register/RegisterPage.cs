using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace TastyBoutique.AutomationTests.PageObjects.Register
{
    public class RegisterPage:BasePage
    {

        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(this.driver, TimeSpan.FromSeconds(10)));
        }

        #region RegisterSection
        [FindsBy(How = How.Name, Using = "email")]
        public IWebElement TxtEmail { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        public IWebElement TxtPassword { get; set; }

        [FindsBy(How=How.Name,Using ="fullname")]
        public IWebElement TxtFullname { get; set; }

        [FindsBy(How = How.Name, Using = "age")]
        public IWebElement TxtAge { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[type='button']")]
        public IWebElement BtnRegister { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".toast-message")]
        public IWebElement ErrInvalidReg { get; set; }
        #endregion


        public void Register(string email, string password,string fullname,string age)
        {
            TxtEmail.SendKeys(email);
            TxtPassword.SendKeys(password);
            TxtFullname.SendKeys(fullname);
            TxtAge.SendKeys(age);
            BtnRegister.Click();
        }
    }
}
