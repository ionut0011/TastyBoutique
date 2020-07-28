using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LinqBuilder.Core;

namespace TastyBoutique.Persistance.Ingredients
{
    public interface IIngredientsRepo : IRepository<Models.Ingredients>
    {
        Task<IList<Models.Ingredients>> Get(ISpecification<Models.Ingredients> spec);

        Task<int> CountAsync();
        Task<Models.Ingredients> GetByName(string Name);

        
    }
}
