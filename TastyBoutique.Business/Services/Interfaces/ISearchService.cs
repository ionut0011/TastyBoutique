using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;

namespace TastyBoutique.Business.Implementations.Services.Interfaces
{
    public interface ISearchService
    {
        //public Task<IList<Ingredients>> MapIngredients(IList<string> ingredientsList);
        public Task<PaginatedList<TotalRecipeModel>> GetRecipiesByQuery(Guid idUser, IList<string> ingredients, SearchModel model);

        public Task<PaginatedList<TotalRecipeModel>> GetRecipiesByFilter(Guid idUser, string filter, SearchModel model);
    }
}
