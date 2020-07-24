using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Recipes.Models.RecipeComment;

namespace TastyBoutique.Business.Recipes.Services.Interfaces
{
    public interface IRecipeCommentService
    {
        Task<IEnumerable<RecipeCommentModel>> Get(Guid IdRecipe);

        Task<RecipeCommentModel> Add(Guid IdRecipe, CreateRecipeCommentModel model);

        Task Delete(Guid IdRecipe, Guid commentId);

    }
}
