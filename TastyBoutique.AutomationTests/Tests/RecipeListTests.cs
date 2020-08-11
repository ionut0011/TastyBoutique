using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.AutomationTests.Helpers;
using TastyBoutique.AutomationTests.PageObjects.Dashboard;
using TastyBoutique.AutomationTests.PageObjects.Login;
using TastyBoutique.AutomationTests.PageObjects.RecipeList;
using Xunit;

namespace TastyBoutique.AutomationTests.Tests
{
    public class RecipeListTests : Browser, IDisposable
    {
        public LoginPage loginPage;
        public DashboardPage dashboardPage;
        public RecipeListPage recipeListPage;
        public RecipeListTests() : base()
        {
            Driver.Navigate()
                .GoToUrl("http://www.tastyboutique.tk/#/login");
            loginPage = new LoginPage(Driver);
        }

        [Fact]
        public void Leave_A_Comment_On_A_Recipe()
        {
            loginPage.Login("testing123@yahoo.com", "Serioux44");
            dashboardPage = new DashboardPage(Driver);
            dashboardPage.ToSeeRecipies();
            recipeListPage = new RecipeListPage(Driver);
            recipeListPage.AddComment("Exceptional");
            Assert.True(recipeListPage.CommentPosted.Displayed);
        }

        [Fact]
        public void Search_By_Ingredients()
        {

            loginPage.Login("testing123@yahoo.com", "Serioux44");
            dashboardPage = new DashboardPage(Driver);
            dashboardPage.ToSeeRecipies();
            recipeListPage = new RecipeListPage(Driver);
            recipeListPage.SearchByIngredients("paine");
            Assert.True(recipeListPage.SuccesForSearchingByIngredients.Displayed);
        }

        [Fact]
        public void Search_By_Filter()
        {

            loginPage.Login("testing123@yahoo.com", "Serioux44");
            dashboardPage = new DashboardPage(Driver);
            dashboardPage.ToSeeRecipies();
            recipeListPage = new RecipeListPage(Driver);
            recipeListPage.SearchByFilters();
            Assert.True(recipeListPage.SuccesForSearchingByFilters.Displayed);
        }

        [Fact]
        public void Add_To_Favorites()
        {
            loginPage.Login("testing123@yahoo.com", "Serioux44");
            dashboardPage = new DashboardPage(Driver);
            dashboardPage.ToSeeRecipies();
            recipeListPage = new RecipeListPage(Driver);
            recipeListPage.AddToFavorites();

            Assert.True(recipeListPage.AddedToFavorites.Displayed);
        }

        public void Dispose()
        {
            CloseBrowser();
        }
    }

}

