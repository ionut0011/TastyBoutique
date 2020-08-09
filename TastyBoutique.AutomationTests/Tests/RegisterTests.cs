using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.AutomationTests.Helpers;
using TastyBoutique.AutomationTests.PageObjects.Dashboard;
using TastyBoutique.AutomationTests.PageObjects.Login;
using TastyBoutique.AutomationTests.PageObjects.Register;
using Xunit;

namespace TastyBoutique.AutomationTests.Tests
{
    public class RegisterTests : Browser, IDisposable
    {
        public LoginPage loginPage;
        public RegisterPage registerPage;
        public DashboardPage dashboardPage;

        public RegisterTests() : base()
        {
            Driver.Navigate()
                .GoToUrl("http://www.tastyboutique.tk/?fbclid=IwAR2lyNONegXkpDwizPAWYMh0zwiEDG9Ybtje6LZ_hlWjPx-gVYuSFJKe5P8#/register");
            registerPage = new RegisterPage(Driver);
        }

        [Fact]
        public void RegisterAndLogin_With_Valid_Credentials()
        {
            registerPage.Register("test1991@yahoo.com","TestAnonim23","TesterDeTeste","14");
            loginPage = new LoginPage(Driver);
            loginPage.WaitForPageToLoad("[class='login-label']");
            loginPage.Login("test1991@yahoo.com", "TestAnonim23");
            dashboardPage = new DashboardPage(Driver);
            dashboardPage.WaitForPageToLoad(".toast-message");
            Assert.True(dashboardPage.LabelLoginSuccessfull.Displayed);
        }

        [Fact]
        public void Register_With_Invalid_Age()
        {
            registerPage.Register("test1998@yahoo.com", "TestAnonim23", "TesterDeTeste", "13");
            registerPage.WaitForPageToLoad(".toast-message");
            Assert.Contains("Something went wrong", registerPage.ErrInvalidReg.Text);
        }

        [Fact]
        public void Register_With_Invalid_Password()
        {
            registerPage.Register("test1948@yahoo.com", "123456", "TesterDeTeste", "14");
            registerPage.WaitForPageToLoad(".toast-message");
            Assert.Contains("Something went wrong", registerPage.ErrInvalidReg.Text);
        }

        [Fact]
        public void Register_With_Invalid_Email()
        {
            registerPage.Register("test1948", "TestAnonim23", "TesterDeTeste", "14");
            registerPage.WaitForPageToLoad(".toast-message");
            Assert.Contains("Something went wrong", registerPage.ErrInvalidReg.Text);
        }
        public void Dispose()
        {
            CloseBrowser();
        }
    }
}
