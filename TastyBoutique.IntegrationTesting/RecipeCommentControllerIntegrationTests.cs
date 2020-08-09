using FluentAssertions;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.RecipeComment;
using TastyBoutique.Persistance.Models;
using Xunit;

namespace TastyBoutique.IntegrationTesting
{
    public class RecipeCommentControllerIntegrationTests : IntegrationTests
    {
        [Fact]
        public async Task Post_Comment()
        {
            //Arrange
            var recipe = await AddRecipe();

            var comment = new CreateRecipeCommentModel()
            {
                Comment = "foarte gustoasa",
                IdUser = AuthenticatedUserId,
                Review = 5,
            };

            //Act
            var response = await HttpClient.PostAsJsonAsync($"api/v1/recipe/{recipe.Id}/comments", comment);

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            var body = await response.Content.ReadAsStringAsync();
            var commentId = Extract_Guid(body);

            RecipeComment existingComment = null;
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                existingComment = await tastyBoutiqueContext.RecipeComments
                    .FirstOrDefaultAsync(c => c.Id == commentId);
            });

            existingComment.Should().NotBeNull();
            existingComment.IdRecipe.Should().Be(recipe.Id);

            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                recipe = await tastyBoutiqueContext.Recipes
                    .FirstOrDefaultAsync(r => r.Id == recipe.Id);
            });
            recipe.ReviewCount.Should().Be(1);
            recipe.AverageReview.Should().Be(comment.Review);
        }

        [Fact]
        public async Task Get_Comment()
        {
            //Arrange
            var recipe = await AddRecipe();
            var comment = new RecipeComment
            {
                Comment = "foarte gustoasa",
                IdRecipe = recipe.Id,
                IdUser = AuthenticatedUserId,
                Review = 5
            };

            recipe.RecipeComment.Add(comment);
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                await tastyBoutiqueContext.AddAsync(comment);
                await tastyBoutiqueContext.SaveChangesAsync();
            });

            //Act
            var response = await HttpClient.GetAsync($"api/v1/recipe/{recipe.Id}/comments");

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var recipes = await response.Content.ReadAsAsync<IList<RecipeCommentModel>>();
            recipe.Should().NotBeNull();
            recipe.Should().NotBeNull();
            recipes.Count.Should().Be(1);
        }

        [Fact]
        public async Task Delete_Comment()
        {
            //Arrange
            var recipe = await AddRecipe();
            var comment = new RecipeComment
            {
                Comment = "foarte gustoasa",
                IdRecipe = recipe.Id,
                IdUser = AuthenticatedUserId,
                Review = 5
            };

            recipe.RecipeComment.Add(comment);
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                await tastyBoutiqueContext.AddAsync(comment);
                await tastyBoutiqueContext.SaveChangesAsync();
            });
            //Act
            var response = await HttpClient.DeleteAsync($"api/v1/recipe/comments/{comment.Id}");
            var content = response.Content.ReadAsStringAsync();

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            RecipeComment existingComment = null;
            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                existingComment = await tastyBoutiqueContext.RecipeComments
                    .FirstOrDefaultAsync(c => c.Id == comment.Id);
            });

            existingComment.Should().BeNull();
        }
    }
}
