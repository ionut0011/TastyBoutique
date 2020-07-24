using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Implementations.Models.Filter;
using TastyBoutique.Business.Implementations.Models.Recipe;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;

namespace TastyBoutique.Business.Recipes.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<RecipeModel> Add(UpsertRecipeModel model, GetPhotoModel pmodel);
        Task<RecipeModel> GetById(Guid id);
        Task<PaginatedList<RecipeModel>> Get(SearchModel model);
        Task Update(Guid id, UpsertRecipeModel model, GetPhotoModel pmodel);
        Task Delete(Guid id);
        Task<PaginatedList<FilterModel>> GetFiltersByRecipeId(Guid id);

        Task<PaginatedList<IngredientModel>> GetIngredientsByRecipeId(Guid id);



    }
}
