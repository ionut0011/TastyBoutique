using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Recipes
{
    public interface IRecipeRepo : IRepository<Models.Recipes>
    {
        Task<IList<Models.Recipes>> Get(ISpecification<Models.Recipes> spec);

        Task<int> CountAsync();

        Task<Models.Recipes> GetByIdWithComments(Guid id);
    }
}
