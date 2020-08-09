using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace TastyBoutique.AutomationTests.PageObjects.Recover
{
    public class RecoverPage :BasePage
    {

        public RecoverPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(this.driver, TimeSpan.FromSeconds(20)));
        }
        #region Recover
        [FindsBy(How = How.Name, Using = "email")]
        public IWebElement TxtEmail { get; set; }

        [FindsBy(How = How.Name, Using = "newpassword")]
        public IWebElement TxtNewPassword { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[type='button']")]
        public IWebElement BtnChangePswd { get; set; }


        [FindsBy(How = How.CssSelector, Using = ".toast-message")]
        public IWebElement ErrInvalidRec { get; set; }
        #endregion

        public void Recover(string email, string password)
        {
            TxtEmail.SendKeys(email);
            TxtNewPassword.SendKeys(password);
            BtnChangePswd.Click();
        }
    }
}
