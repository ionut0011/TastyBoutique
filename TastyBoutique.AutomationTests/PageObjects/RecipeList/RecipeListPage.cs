using System;

using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace TastyBoutique.AutomationTests.PageObjects.RecipeList
{
    public class RecipeListPage:BasePage
    {
        public RecipeListPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(this.driver, TimeSpan.FromSeconds(10)));
        }

        [FindsBy(How = How.XPath, Using = "//app-root/app-recipe-list/div[@class='container']/div[2]/div[1]/div[@class='card']/div[@class='text']/div[@class='btn lets-cook-btn']")]
        public IWebElement BtnLetsCook { get; set; }


        [FindsBy(How = How.XPath, Using = "//app-root/app-recipes-details/div[@class='d-flex justify-content-center justify-content-md-center']/section[@class='form-section']/div[@class='comments-section']/form//input[@type='text']")]
        public IWebElement TxtComment { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".create_new_comment > .stars")]
        public IWebElement Stars { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-root/app-recipes-details[@class='ng-star-inserted']//section[@class='form-section']/div[@class='comments-section']/form//button[@type='button']/span[.='Add comment']")]
        public IWebElement BtnAddComment { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-root/app-recipe-list/div[@class='container']//div[@class='flex']/div[1]//input[@type='text']")]
        public IWebElement TxtSearchByIngredients { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-root/app-recipe-list/div[@class='container']//div[@class='flex']/div[1]//button[@type='button']")]
        public IWebElement BtnSearchByIngredients { get; set; }


        [FindsBy(How = How.XPath, Using = "/html/body/app-root/app-recipe-list/div/div[1]/div/div[2]/div/span/select/option[1]")]
        public IWebElement SlctAreaSearchByFilters { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-root/app-recipe-list/div[@class='container']//div[@class='flex']/div[2]//button[@type='button']")]
        public IWebElement BtnSearchByFilter { get; set; }

        [FindsBy(How=How.CssSelector,Using= ".toast-message")]
        public IWebElement CommentPosted { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".topnav")]
        public IWebElement SuccesForSearchingByIngredients { get; set; }


        [FindsBy(How = How.CssSelector, Using = ".topnav")]
        public IWebElement SuccesForSearchingByFilters { get; set; }

        [FindsBy(How=How.XPath,Using= "//app-root/app-recipe-list[@class='ng-star-inserted']/div[@class='container']/div[2]/div[1]//i[@class='fa fa-heart-o']")]
        public IWebElement LabelAddToFavorites { get; set; }


        [FindsBy(How = How.CssSelector, Using = ".toast-message")]
        public IWebElement AddedToFavorites { get; set; }


        public void AddComment(string comment)
        {
            BtnLetsCook.Click();
            Stars.Submit();
            TxtComment.SendKeys(comment);
            BtnAddComment.Click();
        }

        public void SearchByIngredients(string ingredientsList)
        {
            TxtSearchByIngredients.SendKeys(ingredientsList);
            BtnSearchByIngredients.Click();
        }

        public void SearchByFilters()
        {
            SlctAreaSearchByFilters.Click();
            BtnSearchByFilter.Click();
        }

        public void AddToFavorites()
        {
            LabelAddToFavorites.Click();
        }
    }
}
