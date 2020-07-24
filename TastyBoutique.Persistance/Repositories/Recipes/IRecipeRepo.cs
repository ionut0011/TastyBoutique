using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;

namespace TastyBoutique.Persistance.Recipes
{
    public interface IRecipeRepo : IRepository<Models.Recipes>
    {
        Task<IList<Models.Recipes>> Get(ISpecification<Models.Recipes> spec);

        Task<int> CountAsync();

        Task<IList<Models.RecipesIngredients>> GetIngredientsByRecipeId(Guid id);
        Task<IList<Models.RecipesFilters>> GetFiltersByRecipeId(Guid id);
    }
}
