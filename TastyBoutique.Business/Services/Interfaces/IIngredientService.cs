using System;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.Ingredients;
using TastyBoutique.Business.Models.Shared;

namespace TastyBoutique.Business.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IngredientModel> Add(CreateIngredientModel model);
        Task<PaginatedList<IngredientModel>> Get(SearchModel model);
        //Task<IngredientModel> GetId(PaginatedList<IngredientModel> model, string name);

        //Task<IngredientModel> GetByName(String name);

    }
}
