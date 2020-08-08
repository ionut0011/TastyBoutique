using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.Business.Models.Ingredients;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Persistance.Models;
using Xunit;

namespace TastyBoutique.IntegrationTesting
{
    public class RecipeControllerIntegrationTests : IntegrationTests
    {
        private Recipes recipe;

        public RecipeControllerIntegrationTests()
        {
           
        }

        [Fact]
        public async Task Add_Recipe()
        {
            //Arange
            var recipe = new UpsertRecipeModel
            {
                Name = "Salata",
                Access = true,
                Description = "Simpla si gustoasa, perfecta pentru zilele de vara",
                Filter = "Vegana",
                IngredientsList = new List<string> { "ceapa", "ulei", "rosii", "varza", "castraveti" },
                Type = "meal",
                Image = new byte[] { 1, 0, 0, 1 }
            };

            var response = await HttpClient.PostAsJsonAsync($"api/v1/recipe", recipe);

            response.IsSuccessStatusCode.Should().BeTrue();
            var body = await response.Content.ReadAsStringAsync();

            var createdRecipeId = Extract_Guid(body);

            Recipes existingRecipe = null;
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                existingRecipe = await tastyBoutiqueContext.Recipes
                    .Include(r => r.RecipesFilters)
                    .ThenInclude(r => r.Filter)
                    .Include(r => r.RecipesIngredients)
                    .ThenInclude(r => r.Ingredient)
                    .FirstOrDefaultAsync(r => r.Id == createdRecipeId);
            });
            existingRecipe.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_Recipes()
        {
            //Arange

            //Act
            var response = await HttpClient.GetAsync($"api/v1/recipe");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var recipes = await response.Content.ReadAsAsync<IList<TotalRecipeModel>>();
            recipes.Should().NotBeNull();
            recipes.Count.Should().Be(1);
        }

        [Fact]
        public async Task Update_Recipe()
        {
            //Arange
            var recipe = new UpsertRecipeModel
            {
                Name = "Salata",
                Access = true,
                Description = "Simpla si gustoasa, perfecta pentru zilele de vara",
                Filter = "Vegana",
                IngredientsList = new List<string> { "ceapa", "ulei", "rosii", "varza", "castraveti" },
                Type = "meal",
                Image = new byte[] { 1, 0, 0, 1 }
            };

            var response = await HttpClient.PostAsJsonAsync($"api/v1/recipe", recipe);

            response.IsSuccessStatusCode.Should().BeTrue();
            var body = await response.Content.ReadAsStringAsync();

            var createdRecipeId = Extract_Guid(body);
            Recipes existingRecipe = null;
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                existingRecipe = await tastyBoutiqueContext.Recipes
                    .Include(r => r.RecipesFilters)
                    .ThenInclude(r => r.Filter)
                    .Include(r => r.RecipesIngredients)
                    .ThenInclude(r => r.Ingredient)
                    .FirstOrDefaultAsync(r => r.Id == createdRecipeId);
            });
            existingRecipe.Should().NotBeNull();

            //Act
            recipe.IngredientsList.Add("branza");
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(recipe), Encoding.UTF8, "application/json-patch+json");
            response = await HttpClient.PatchAsync($"api/v1/recipe/{createdRecipeId}", httpContent);

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                existingRecipe = await tastyBoutiqueContext.Recipes
                    .Include(r => r.RecipesIngredients)
                    .ThenInclude(r => r.Ingredient)
                    .FirstOrDefaultAsync(r => r.Id == createdRecipeId);
            });

            existingRecipe.RecipesIngredients.Select(ri => ri.Ingredient.Name).Should().Contain("branza");
        }

        [Fact]
        public async Task Delete_Recipe()
        {
            //Arrange
            await AddRecipe();

            //Act
            var response = await HttpClient.DeleteAsync($"api/v1/recipe/{this.recipe.Id}");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            Recipes recipe = null;
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                recipe = await tastyBoutiqueContext.Recipes
                    .Include(r => r.RecipesFilters)
                    .ThenInclude(r => r.Filter)
                    .Include(r => r.RecipesIngredients)
                    .ThenInclude(r => r.Ingredient)
                    .FirstOrDefaultAsync(r => r.Id == this.recipe.Id);
            });

            recipe.Should().BeNull();
        }

        [Fact]
        public async Task Get_Recipe_By_Id()
        {
            //Arange
            await AddRecipe();

            //Act
            var response = await HttpClient.GetAsync($"api/v1/recipe/{this.recipe.Id}");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var recipes = await response.Content.ReadAsAsync<TotalRecipeModel>();
            recipes.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_Recipes_Ingredients()
        {
            //Arrange
            await AddRecipe();

            //Act
            var response = await HttpClient.GetAsync($"api/v1/recipe/{this.recipe.Id}/ingredients");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var ingredients = await response.Content.ReadAsAsync<IList<IngredientModel>>();
            ingredients.Should().NotBeNull();
            ingredients.Count.Should().Be(this.recipe.RecipesIngredients.Count());
            ingredients.Select(i => i.Name).All(name => recipe.RecipesIngredients.Select(r => r.Ingredient.Name).Contains(name))
                .Should().BeTrue();
        }

        [Fact]
        public async Task Get_Recipes_Filters()
        {
            //Arange
            await AddRecipe();

            //Act
            var response = await HttpClient.GetAsync($"api/v1/recipe/{this.recipe.Id}/filters");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var filters = await response.Content.ReadAsAsync<IList<FilterModel>>();
            filters.Should().NotBeNull();
            filters.Count.Should().Be(this.recipe.RecipesFilters.Count());
            filters.Select(f => f.Name).All(name => recipe.RecipesFilters.Select(r => r.Filter.Name).Contains(name))
                .Should().BeTrue();
        }

        private async Task AddRecipe()
        {
            var image = new byte[] { 1, 0, 0, 1, 8 };
            recipe = new Recipes("Cartofi copti", "meal", true, "Foarte buni", image);
            recipe.RecipesIngredients.Add(new RecipesIngredients(recipe, new Ingredients("cartofi")));
            recipe.RecipesIngredients.Add(new RecipesIngredients(recipe, new Ingredients("ulei")));
            recipe.RecipesIngredients.Add(new RecipesIngredients(recipe, new Ingredients("morcov")));
            recipe.RecipesIngredients.Add(new RecipesIngredients(recipe, new Ingredients("ceapa")));
            recipe.RecipesFilters.Add(new RecipesFilters(recipe, new Filters("vegan")));
            recipe.IdUser = AuthenticatedUserId;

            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                await tastyBoutiqueContext.Recipes.AddAsync(recipe);
                await tastyBoutiqueContext.SaveChangesAsync();
            });
        }
    }
}
