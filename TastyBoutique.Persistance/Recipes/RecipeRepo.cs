using System.Collections.Generic;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Recipes
{
    public sealed class RecipeRepo : Repository<Models.Recipes>, IRecipeRepo
    {
        public RecipeRepo(TastyBoutique_v2Context context) : base(context) { }

        public async Task<IList<Models.Recipes>> Get(ISpecification<Models.Recipes> spec)
            => await this.context.Recipes.ExeSpec(spec).ToListAsync();

        public async Task<int> CountAsync()
            => await this.context.Recipes.CountAsync();
    }
}
