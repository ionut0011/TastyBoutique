using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LinqBuilder.Core;

namespace TastyBoutique.Persistance.Repositories.Filters
{
    public interface IFiltersRepo : IRepository<Models.Filters>
    {
        Task<IList<Models.Filters>> Get(ISpecification<Models.Filters> spec);

        Task<int> CountAsync();

        Task<IList<Models.Filters>> GetFiltersAsList();

    }
}
