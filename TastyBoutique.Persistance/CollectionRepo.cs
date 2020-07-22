using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance
{
    public sealed class CollectionRepo : Repository<Models.SavedRecipes>, ICollectionRepo
    {
        public CollectionRepo(TastyBoutique_v2Context context) : base(context) { }

        public async Task<IList<Models.SavedRecipes>> Get(ISpecification<Models.SavedRecipes> spec)
            => await this.context.SavedRecipes.ExeSpec(spec).ToListAsync();

        public async Task<int> CountAsync()
            => await this.context.Recipes.CountAsync();
    }
}
