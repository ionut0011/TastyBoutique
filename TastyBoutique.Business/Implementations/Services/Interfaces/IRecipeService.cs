using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Recipes.Models.Recipe;

namespace TastyBoutique.Business.Recipes.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<RecipeModel> Add(UpsertRecipeModel model);
        Task<RecipeModel> GetById(Guid id);
        Task<PaginatedList<RecipeModel>> Get(SearchModel model);
        Task Update(Guid id, UpsertRecipeModel model);
        Task Delete(Guid id);
    }
}
