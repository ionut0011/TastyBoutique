using System;

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
        [FindsBy(How = How.ClassName, Using = "headerLogo")]
        public IWebElement HeaderLogo { get; set; }

        #endregion


    }
}
