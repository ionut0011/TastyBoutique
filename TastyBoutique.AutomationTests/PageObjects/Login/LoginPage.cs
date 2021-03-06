﻿using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace TastyBoutique.AutomationTests.PageObjects.Login
{
    public class LoginPage : BasePage
    {


        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(this.driver, TimeSpan.FromSeconds(10)));
        }

        #region Login section
        [FindsBy(How = How.Name, Using = "email")]
        public IWebElement TxtEmail { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        public IWebElement TxtPassword { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[type='button']")]
        public IWebElement BtnLogin { get; set; }

        [FindsBy(How = How.ClassName, Using = "login-label")]
        public IWebElement LoginInterface { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".toast-message")]
        public IWebElement ErrInvalidCred { get; set; }
        #endregion


        public void Login(string email, string password)
        {
            TxtEmail.SendKeys(email);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
        }

    }

}
