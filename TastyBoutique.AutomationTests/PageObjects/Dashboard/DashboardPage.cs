﻿using System;

using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace TastyBoutique.AutomationTests.PageObjects.Dashboard
{
    public class DashboardPage : BasePage
    {


        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(this.driver, TimeSpan.FromSeconds(10)));
        }

        #region Dashboard section
        
        [FindsBy(How = How.CssSelector, Using = ".toast-message")]
        public IWebElement LabelLoginSuccessfull { get; set; }

        [FindsBy(How=How.XPath,Using= "//app-root/app-dashboard//app-tile[@label='Create Recipe']//div[@class='header']//mat-icon[@role='img']")]
        public IWebElement LabelCreateRecipe { get; set; }

        
        [FindsBy(How = How.XPath, Using = "//app-root/app-dashboard[@class='ng-star-inserted']//app-tile[@label='Recipes']//div[@class='header']//mat-icon[@role='img']")]
        public IWebElement LabelRecipies { get; set; }



        public void ToCreateRecipe()
        {
            LabelCreateRecipe.Click();
        }

        public void ToSeeRecipies()
        {
            LabelRecipies.Click();
        }


        #endregion


    }
}
