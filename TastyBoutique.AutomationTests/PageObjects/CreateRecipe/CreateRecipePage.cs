using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace TastyBoutique.AutomationTests.PageObjects.CreateRecipe
{
   public  class CreateRecipePage :BasePage
    {


        public CreateRecipePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(this.driver, TimeSpan.FromSeconds(10)));
        }


        #region CreateRecipe section
        [FindsBy(How = How.XPath, Using = "//app-root/app-recipes-details//section[@class='form-section']/form//input[@type='text']")]
        public IWebElement TxtRecipeName { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-root/app-recipes-details//section[@class='form-section']/form//textarea[@placeholder='Enter ingredients']")]
        public IWebElement TxtIngredients { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-root/app-recipes-details//section[@class='form-section']/form//button[.='Add']")]
        public IWebElement BtnAdd { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-root/app-recipes-details//section[@class='form-section']/form/div[2]/div[2]/select")]
        public IWebElement LabelFilters { get; set; }

        [FindsBy(How = How.XPath, Using= "/html/body/app-root/app-recipes-details/div/section/form/div[2]/div[2]/select/option[1]")]
        public IWebElement FiltersResults { get; set; }


        [FindsBy(How = How.XPath, Using = "/html/body/app-root/app-recipes-details/div/section/form/div[2]/div[3]/select/option[1]")]
        public IWebElement TypeResults { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-root/app-recipes-details//section[@class='form-section']/form/div[2]/div[3]/select")]
        public IWebElement LabelSelectType { get; set; }


        [FindsBy(How = How.XPath, Using = "//app-root/app-recipes-details//section[@class='form-section']/form//textarea[@placeholder='Enter recipe description']")]
        public IWebElement TxtRecipeDescription { get; set; }

        [FindsBy(How = How.XPath, Using= "//app-root/app-recipes-details//section[@class='form-section']/form//span[.='Save Recipe']")]
        public IWebElement ButtonSaveRecipe { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".toast-message")]
        public IWebElement LabelRecipeAdded { get; set; }
        #endregion


        public void AddFilters()
        {
            LabelFilters.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(FiltersResults));
            FiltersResults.Click();
        }

        public void AddType()
        {
            LabelSelectType.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(FiltersResults));
            TypeResults.Click();
        }

        public void CreateRecipe(string recipeTitle,string recipeIngredients,string recipeDescription)
        {
            TxtRecipeName.SendKeys(recipeTitle);
            TxtIngredients.SendKeys(recipeIngredients);
            BtnAdd.Click();
            AddFilters();
            AddType();
            TxtRecipeDescription.SendKeys(recipeDescription);
            ButtonSaveRecipe.Click();
        }
    }
}
