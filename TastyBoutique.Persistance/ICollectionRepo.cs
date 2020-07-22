using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LinqBuilder.Core;

namespace TastyBoutique.Persistance
{
    public interface ICollectionRepo : IRepository<Models.SavedRecipes>
    {
        Task<IList<Models.SavedRecipes>> Get(ISpecification<Models.SavedRecipes> spec);

        Task<int> CountAsync();
    }
}
