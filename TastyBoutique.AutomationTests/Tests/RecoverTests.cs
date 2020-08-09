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
                .GoToUrl("http://www.tastyboutique.tk/?fbclid=IwAR2lyNONegXkpDwizPAWYMh0zwiEDG9Ybtje6LZ_hlWjPx-gVYuSFJKe5P8#/recover");
            recoverPage = new RecoverPage(Driver);
        }

        [Fact]
        public void Recover_With_Valid_Credentials()
        {
            recoverPage.Recover("chelaru.george1998@yahoo.com","Serioux33");
            loginPage = new LoginPage(Driver);
            loginPage.WaitForPageToLoad(".toast-message");
            loginPage.Login("chelaru.george1998@yahoo.com", "Serioux33");
            dashboardPage = new DashboardPage(Driver);
            dashboardPage.WaitForPageToLoad(".toast-message");
            Assert.True(dashboardPage.LabelLoginSuccessfull.Displayed);
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
