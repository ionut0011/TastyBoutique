using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.AutomationTests.Helpers;
using TastyBoutique.AutomationTests.PageObjects.Dashboard;
using TastyBoutique.AutomationTests.PageObjects.Login;
using Xunit;

namespace TastyBoutique.AutomationTests.Tests
{
    public class LoginTests : Browser, IDisposable
    {
        public LoginPage loginPage;
        public DashboardPage dashboardPage;
        public LoginTests() : base()
        {
            Driver.Navigate()
                .GoToUrl("http://www.tastyboutique.tk/?fbclid=IwAR2lyNONegXkpDwizPAWYMh0zwiEDG9Ybtje6LZ_hlWjPx-gVYuSFJKe5P8#/login");
            loginPage = new LoginPage(Driver);
        }

        [Fact]
        public void Login_With_Valid_Credentials()
        {
            loginPage.Login("chelaru.george1998@yahoo.com", "Serioux22");
            dashboardPage = new DashboardPage(Driver);
            dashboardPage.WaitForPageToLoad(".toast-message");
            Assert.True(dashboardPage.LabelLoginSuccessfull.Displayed);
        }

        [Fact]
        public void Login_With_Invalid_Password()
        {
            loginPage.Login("chelaru.george1998@yahoo.com", "test");
            loginPage.WaitForPageToLoad(".toast-message");
            Assert.Contains("Wrong email or password", loginPage.ErrInvalidCred.Text);
        }
        public void Dispose()
        {
            CloseBrowser();
        }
    }
}
