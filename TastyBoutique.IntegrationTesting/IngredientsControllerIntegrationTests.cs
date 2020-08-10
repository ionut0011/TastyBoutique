using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.Ingredients;
using TastyBoutique.Persistance.Models;
using Xunit;

namespace TastyBoutique.IntegrationTesting
{
    public class IngredientsControllerIntegrationTests : IntegrationTests
    {
        [Fact]
        public async Task Post_Ingredient()
        {
            //Arrange
            var ingredientModel = new CreateIngredientModel { Name = "Ceapa" };

            //Act
            var response = await HttpClient.PostAsJsonAsync($"api/v1/ingredient", ingredientModel);

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            var ingredientId = Guid.Parse(response.Headers.Location.OriginalString);
            Ingredients ingredient = null;
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                ingredient = await tastyBoutiqueContext.Ingredients
                    .FirstOrDefaultAsync(i => i.Id == ingredientId);
            });

            ingredient.Should().NotBeNull();
        }
        [Fact]
        public async Task Get_Ingredients()
        {
            //Arrange
            var ingredient = new Ingredients
            {
                Name = "Ceapa"
            };
            var ingredient2 = new Ingredients
            {
                Name = "Branza"
            };

            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                await tastyBoutiqueContext.AddAsync(ingredient);
                await tastyBoutiqueContext.AddAsync(ingredient2);
                await tastyBoutiqueContext.SaveChangesAsync();
            });

            //Act
            var response = await HttpClient.GetAsync("api/v1/ingredient");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            var ingredients = await response.Content.ReadAsAsync<IList<IngredientModel>>();
            ingredients.Should().NotBeNull();
            ingredients.Should().NotBeEmpty();
            ingredients.Should().HaveCount(2);
            ingredients.Select(i => i.Name).Should().Contain(ingredient.Name);
            ingredients.Select(i => i.Name).Should().Contain(ingredient2.Name);
        }
    }
}
