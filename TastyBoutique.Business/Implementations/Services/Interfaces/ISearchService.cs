using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Implementations.Models;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Business.Implementations.Services.Interfaces
{
    public interface ISearchService
    {
        public Task<IList<Ingredients>> MapIngredients(IList<string> ingredientsList);
        public Task<IList<Filters>> MapFilters(IList<string> filtersList);
        public Task<PaginatedList<RecipeModel>> GetRecipiesByQuery(RecipeSearchModel query, SearchModel model);
    }
}
