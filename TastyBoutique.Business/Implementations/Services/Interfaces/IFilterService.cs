using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Implementations.Models.Filter;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;

namespace TastyBoutique.Business.Implementations.Services.Interfaces
{
    public interface IFilterService
    {
        Task<FilterModel> Add(CreateFilterModel model);
        Task<PaginatedList<FilterModel>> Get(SearchModel model);
        Task<FilterModel> GetId(PaginatedList<FilterModel> model, string name);

       Task<FilterModel> GetFilterByName(String name);
    }
}
