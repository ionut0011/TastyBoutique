using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
    public class CollectionControllerIntegrationTests : IntegrationTests
    {
        [Fact]
        public async Task Add_to_Collection()
        {
            //Arrange
            var recipe = await AddRecipe();
            var model = new SavedRecipeModel { IdRecipe = recipe.Id };
            //Act
            var response = await HttpClient.PostAsJsonAsync($"api/v1/collections", model );

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            SavedRecipes savedRecipe = null;
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                savedRecipe = await tastyBoutiqueContext.SavedRecipes
                    .FirstOrDefaultAsync(sr => sr.IdUser == AuthenticatedUserId && sr.IdRecipe == recipe.Id);
            });

            savedRecipe.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_from_Collection()
        {
            //Arrange
            var recipe1 = await AddRecipe();
            var recipe2 = await AddRecipe();

            SavedRecipes savedRecipe1 = new SavedRecipes
            {
                IdRecipe = recipe1.Id,
                IdUser = AuthenticatedUserId
            };
            SavedRecipes savedRecipe2 = new SavedRecipes
            {
                IdRecipe = recipe2.Id,
                IdUser = AuthenticatedUserId
            };

            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                await tastyBoutiqueContext.AddAsync(savedRecipe1);
                await tastyBoutiqueContext.AddAsync(savedRecipe2);
                tastyBoutiqueContext.SaveChanges();
            });

            //Act
            var response = await HttpClient.GetAsync($"api/v1/collections");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            var savedRecipes = await response.Content.ReadAsAsync<IList<TotalRecipeModel>>();
            savedRecipes.Should().NotBeNull();
            savedRecipes.Should().NotBeEmpty();
            savedRecipes.Should().HaveCount(2);
            //savedRecipes.Select(r => r.Id).Should().Contain(savedRecipe1.IdRecipe);
            //savedRecipes.Select(r => r.Id).Should().Contain(savedRecipe2.IdRecipe);
        }

        [Fact]
        public async Task Delete_from_Collection()
        {
            //Arrange
            var recipe1 = await AddRecipe();

            SavedRecipes savedRecipe1 = new SavedRecipes
            {
                IdRecipe = recipe1.Id,
                IdUser = AuthenticatedUserId
            };

            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                await tastyBoutiqueContext.AddAsync(savedRecipe1);
                tastyBoutiqueContext.SaveChanges();
            });

            //Act
            var response = await HttpClient.DeleteAsync($"api/v1/collections/{recipe1.Id}");

            //Assert
            savedRecipe1 = null;
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                savedRecipe1 = await tastyBoutiqueContext.SavedRecipes
                    .FirstOrDefaultAsync(sr => sr.IdRecipe == recipe1.Id && sr.IdUser == AuthenticatedUserId);
            });
            savedRecipe1.Should().BeNull();
        }
    }
}
