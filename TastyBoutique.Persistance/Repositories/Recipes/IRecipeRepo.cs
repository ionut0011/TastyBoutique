using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Recipes
{
    public interface IRecipeRepo : IRepository<Models.Recipes>
    {
        Task<IList<Models.Recipes>> Get(ISpecification<Models.Recipes> spec);

        Task<int> CountAsync();

        Task<IList<Models.RecipesIngredients>> GetIngredientsByRecipeId(Guid id);
        Task<IList<Models.RecipesFilters>> GetFiltersByRecipeId(Guid id);

        Task<IList<Models.Recipes>> GetRecipesUnpaginated();
        Task<List<Models.RecipeComment>> GetCommentsReview(Guid idRecipe);
        Task<Models.RecipeType> GetRecipeTypeById(Guid id);
        Task<Models.Recipes> GetByIdWithComments(Guid id);

        Task<List<Models.Recipes>> GetRecipiesByQuery(IList<Models.Ingredients> ingredients, IList<Filters> filters);

      
    }
}
