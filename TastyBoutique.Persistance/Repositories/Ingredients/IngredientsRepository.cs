using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Ingredients
{
    public sealed class IngredientsRepository : Repository<Models.Ingredients>, IIngredientsRepository
    {

        public IngredientsRepository(TastyBoutiqueContext context) : base(context) { }

        public async Task<IList<Models.Ingredients>> Get(ISpecification<Models.Ingredients> spec)
            => await this.context.Ingredients.ExeSpec(spec).ToListAsync();

        public async Task<int> CountAsync()
            => await this.context.Ingredients.CountAsync();

        public async Task<Models.Ingredients> GetByName(String name)
            => await this.context.Ingredients.Where(i => i.Name.Equals(name)).FirstOrDefaultAsync();

    }
}
