using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
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

        public async Task<IList<Models.RecipesFilters>> GetFiltersByRecipeId(Guid id)
            => await this.context.RecipesFilters
                .Include(i => i.Filter)
                .Where(i => i.RecipeId == id)
                .ToListAsync();

        public async Task<IList<Models.RecipesIngredients>> GetIngredientsByRecipeId(Guid id)
            => await this.context.RecipesIngredients
                .Include(i => i.Ingredient)
                .Where(i => i.RecipeId == id)
                .ToListAsync();
        public async Task<Models.Ingredients> GetByName(string Name)
            => await this.context.Ingredients.Where(i => i.Name.Equals(Name)).FirstAsync();

        public async Task<Models.Recipes> GetByIdWithComments(Guid id)
          => await this.context.Recipes
          .Include(recipe => recipe.RecipeComment)
          .FirstAsync(recipe => recipe.Id == id);
    }
}
