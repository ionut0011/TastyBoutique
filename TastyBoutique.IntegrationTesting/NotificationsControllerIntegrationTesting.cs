using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Persistance.Models;
using Xunit;

namespace TastyBoutique.IntegrationTesting
{
    public class NotificationsControllerIntegrationTesting : IntegrationTests
    {
        [Fact]
        public async Task Get_Notifications()
        {
            //Arange
            var recipe = new UpsertRecipeModel
            {
                Name = "Salata",
                Access = true,
                Description = "Simpla si gustoasa, perfecta pentru zilele de vara",
                Filter = "Vegana",
                Ingredients = new List<string> { "ceapa", "ulei", "rosii", "varza", "castraveti" },
                Type = "meal",
                Image = new byte[] { 1, 0, 0, 1 }
            };

            var response = await HttpClient.PostAsJsonAsync($"api/v1/recipe", recipe);
            var body = await response.Content.ReadAsStringAsync();
            var createdRecipeId = Extract_Guid(body);
            var savedRecipe = new SavedRecipes
            {
                IdUser = AuthenticatedUserId,
                IdRecipe = createdRecipeId
            };

            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                await tastyBoutiqueContext.AddAsync(savedRecipe);
                tastyBoutiqueContext.SaveChanges();
            });

            recipe.Ingredients.Add("branza");
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(recipe), Encoding.UTF8, "application/json-patch+json");
            response = await HttpClient.PatchAsync($"api/v1/recipe/{createdRecipeId}", httpContent);

            //Act
            response = await HttpClient.GetAsync("api/v1/notifications");
            response.IsSuccessStatusCode.Should().BeTrue();
            var notifications = await response.Content.ReadAsAsync<IList<TotalRecipeModel>>();

            //Assert
            notifications.Should().NotBeNull();
            notifications.Should().NotBeEmpty();
            notifications.Count.Should().Be(1);
            notifications[0].Name.Should().Be(recipe.Name);
        }

        [Fact]
        public async Task Get_When_No_Notifications()
        {
            //Arange

            //Act
            var response = await HttpClient.GetAsync("api/v1/notifications");
            response.IsSuccessStatusCode.Should().BeTrue();
            var notifications = await response.Content.ReadAsAsync<IList<TotalRecipeModel>>();

            //Assert
            notifications.Should().NotBeNull();
            notifications.Should().BeEmpty();
        }

        [Fact]
        public async Task Patch_Notification()
        {
            //Arrange
            var recipe = await AddRecipe();
            var savedRecipe = new SavedRecipes
            {
                IdRecipe = recipe.Id,
                IdUser = AuthenticatedUserId,
                NeedUpdate = true
            };

            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                await tastyBoutiqueContext.AddAsync(savedRecipe);
                tastyBoutiqueContext.SaveChanges();
            });

            //Act
            var response = await HttpClient.PatchAsync($"api/v1/notifications/{recipe.Id}", null);

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                savedRecipe = await tastyBoutiqueContext.SavedRecipes
                .FirstOrDefaultAsync(sr => sr.IdRecipe == recipe.Id && sr.IdUser == AuthenticatedUserId);
            });
            savedRecipe.NeedUpdate.Should().BeFalse();

            response = await HttpClient.GetAsync($"api/v1/notifications");
            var result = await response.Content.ReadAsAsync<IList<TotalRecipeModel>>();

            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }
    }
}
