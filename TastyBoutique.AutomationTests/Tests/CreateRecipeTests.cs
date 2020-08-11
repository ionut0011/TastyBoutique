using System;

using TastyBoutique.AutomationTests.Helpers;
using TastyBoutique.AutomationTests.PageObjects.CreateRecipe;
using TastyBoutique.AutomationTests.PageObjects.Dashboard;
using TastyBoutique.AutomationTests.PageObjects.Login;
using Xunit;

namespace TastyBoutique.AutomationTests.Tests
{
    public class CreateRecipeTests : Browser, IDisposable
    {
        public LoginPage loginPage;
        public DashboardPage dashboardPage;
        public CreateRecipePage createRecipePage;

        public CreateRecipeTests() : base()
        {
            Driver.Navigate()
                .GoToUrl("http://www.tastyboutique.tk/#/login");
            loginPage = new LoginPage(Driver);
        }

        [Fact]
        public void Create_Recipe()
        {
            loginPage.Login("testing123@yahoo.com","Serioux44");
            dashboardPage= new DashboardPage(Driver);
            dashboardPage.ToCreateRecipe();
            createRecipePage = new CreateRecipePage(Driver);
            createRecipePage.CreateRecipe("ToastTest", "paine,oua,sunca", "la tigaie");
            Assert.True(createRecipePage.LabelRecipeAdded.Displayed);

        }


        public void Dispose()
        {
            CloseBrowser();
        }

    }
}
