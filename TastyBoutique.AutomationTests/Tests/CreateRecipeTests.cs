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
                .GoToUrl("http://www.tastyboutique.tk/#/create-recipe");
            createRecipePage = new CreateRecipePage(Driver);
        }

        [Fact]
        public void Create_Recipe()
        {
           
            createRecipePage.CreateRecipe("ToastTest", "paine,oua,sunca", "la tigaie");
            Assert.True(createRecipePage.LabelRecipeAdded.Displayed);

        }


        public void Dispose()
        {
            CloseBrowser();
        }

    }
}
