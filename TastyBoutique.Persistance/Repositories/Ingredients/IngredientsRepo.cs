using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Ingredients
{
    public sealed class IngredientsRepo : Repository<Models.Ingredients>, IIngredientsRepo
    {

        public IngredientsRepo(TastyBoutique_v2Context context) : base(context) { }

        public async Task<IList<Models.Ingredients>> Get(ISpecification<Models.Ingredients> spec)
            => await this.context.Ingredients.ExeSpec(spec).ToListAsync();

        public async Task<int> CountAsync()
            => await this.context.Ingredients.CountAsync();

        public async Task<Models.Ingredients> GetByName(string Name)
            => await this.context.Ingredients.Where(i => i.Name.Equals(Name)).FirstAsync();

        public async Task<IList<Models.Ingredients>> GetIngredientsAsList()
            => await this.context.Ingredients.ToListAsync();
    }
}
