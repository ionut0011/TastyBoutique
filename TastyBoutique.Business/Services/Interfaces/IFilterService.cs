using System;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.Business.Models.Shared;

namespace TastyBoutique.Business.Services.Interfaces
{
    public interface IFilterService
    {
        Task<FilterModel> Add(CreateFilterModel model);
        Task<PaginatedList<FilterModel>> Get(SearchModel model);
        Task<FilterModel> GetId(PaginatedList<FilterModel> model, string name);

       Task<FilterModel> GetFilterByName(String name);
    }
}
