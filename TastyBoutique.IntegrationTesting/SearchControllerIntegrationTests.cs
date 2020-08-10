using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Persistance.Models;
using Xunit;

namespace TastyBoutique.IntegrationTesting
{
    public class SearchControllerIntegrationTests : IntegrationTests
    {
        [Fact]
        public async Task Search_Without_Ingredients()
        {
            //Arrange
            await AddRecipe();
            await AddRecipe();

            //Act
            var response = await HttpClient.GetAsync($"api/v1/search");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var result = await response.Content.ReadAsAsync<IList<TotalRecipeModel>>();

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count.Should().Be(2);
        }

        [Fact]
        public async Task Search_By_Multiple_Existing_Ingredients()
        {
            //Arrange
            var recipe = await AddRecipe();

            //Act
            string query = string.Join("query=", recipe.Ingredients.Select(i => i.Ingredient.Name));
            var response = await HttpClient.GetAsync($"api/v1/search?{query}");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var result = await response.Content.ReadAsAsync<IList<TotalRecipeModel>>();

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count.Should().Be(1);
            var ingredients = result[0].Ingredients;
            ingredients.Select(i => i.Name).All(name => recipe.Ingredients.Select(r => r.Ingredient.Name).Contains(name))
                .Should().BeTrue();
        }

        [Fact]
        public async Task Search_By_One_Existing_Ingredient()
        {
            //Arrange
            var recipe = await AddRecipe();

            //Act
            string query = $"query={recipe.Ingredients.ElementAt(0).Ingredient.Name}";
            var response = await HttpClient.GetAsync($"api/v1/search?{query}");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var result = await response.Content.ReadAsAsync<IList<TotalRecipeModel>>();

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count.Should().Be(1);
            result[0].Ingredients.Select(ri => ri.Name).Should().Contain(recipe.Ingredients.ElementAt(0).Ingredient.Name);
        }


        [Fact]
        public async Task Search_By_NonExisting_Ingredient()
        {
            //Arrange
            var recipe = await AddRecipe();

            //Act
            string query = $"query=ardei";
            var response = await HttpClient.GetAsync($"api/v1/search?{query}");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var result = await response.Content.ReadAsAsync<IList<TotalRecipeModel>>();

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Search_By_Existing_Filter()
        {
            //Arrange
            var recipe = await AddRecipe();

            //Act
            string filter = recipe.Filters.ElementAt(0).Filter.Name;
            var response = await HttpClient.GetAsync($"api/v1/search/{filter}");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var result = await response.Content.ReadAsAsync<IList<TotalRecipeModel>>();

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count.Should().Be(1);
            result[0].Filters.Select(ri => ri.Name).Should().Contain(filter);
        }

        [Fact]
        public async Task Search_By_NonExisting_Filter()
        {
            //Arrange
            var recipe = await AddRecipe();

            //Act
            string filter = "test";
            var response = await HttpClient.GetAsync($"api/v1/search/{filter}");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var result = await response.Content.ReadAsAsync<IList<TotalRecipeModel>>();

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
