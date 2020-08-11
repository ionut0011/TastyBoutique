using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.AutomationTests.Helpers;
using TastyBoutique.AutomationTests.PageObjects.Dashboard;
using TastyBoutique.AutomationTests.PageObjects.Login;
using TastyBoutique.AutomationTests.PageObjects.Recover;
using Xunit;

namespace TastyBoutique.AutomationTests.Tests
{
    public class RecoverTests : Browser, IDisposable
    {
        public LoginPage loginPage;
        public DashboardPage dashboardPage;
        public RecoverPage recoverPage;
        public RecoverTests() : base()
        {
            Driver.Navigate()
                .GoToUrl("http://www.tastyboutique.tk/#/recover");
            recoverPage = new RecoverPage(Driver);
        }

        [Fact]
        public void Recover_With_Valid_Credentials()
        {
            recoverPage.Recover("testing1234@yahoo.com","Serioux44");
            loginPage = new LoginPage(Driver);
            loginPage.WaitForPageToLoad(".login-label");
            Assert.True(loginPage.LoginInterface.Displayed);
        }

        [Fact]
        public void Recover_With_Invalid_Email()
        {
            recoverPage.Recover("asdasda@yahoo.com", "TestAnonim123");
            recoverPage.WaitForPageToLoad(".toast-message");
            Assert.Contains("Something went wrong", recoverPage.ErrInvalidRec.Text);
        }

        [Fact]
        public void Recover_With_Invalid_Password()
        {
            recoverPage.Recover("chelaru.george1998@yahoo.com", "test");
            recoverPage.WaitForPageToLoad(".toast-message");
            Assert.Contains("Something went wrong", recoverPage.ErrInvalidRec.Text);
        }
        public void Dispose()
        {
            CloseBrowser();
        }
    }
}
