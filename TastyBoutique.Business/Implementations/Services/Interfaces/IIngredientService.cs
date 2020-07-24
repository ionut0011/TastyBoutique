using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Persistance;

namespace TastyBoutique.Business.Recipes.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IngredientModel> Add(CreateIngredientModel model);
        Task<PaginatedList<IngredientModel>> Get(SearchModel model);
        Task<IngredientModel> GetId(PaginatedList<IngredientModel> model, string name);

        Task<PaginatedList<IngredientModel>> GetIngredientsByRecipeId(Guid id);

    }
}
