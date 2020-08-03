using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Internal;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Recipes
{
    public sealed class RecipeRepo : Repository<Models.Recipes>, IRecipeRepo
    {
        public RecipeRepo(TastyBoutique_v2Context context) : base(context) { }
        private int count = 0;
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
   
        public async Task<Models.Recipes> GetByIdWithComments(Guid id)
          => await this.context.Recipes
              .Include(recipe => recipe.RecipeComment)
              .FirstOrDefaultAsync(recipe => recipe.Id == id);

        public async Task<Models.RecipeType> GetRecipeTypeById(Guid id)
            => await this.context.RecipeType
                .FirstOrDefaultAsync(recipeType => recipeType.RecipeId == id);

        public async Task<List<Models.Recipes>> GetRecipiesByQuery(IList<Models.Ingredients> ingredients, IList<Filters> filters)
        {
            var getRecipes = await this.context.Recipes
                .Include(r => r.RecipesIngredients)
                .Include(r=>r.RecipesFilters)
                .ToListAsync();

            List<Guid> ingredientsIds = null;
            List<Guid> filtersIds = null;

            if (ingredients != null)
            {
                ingredientsIds = ingredients.Select(ingredient => ingredient.Id).ToList();
                getRecipes = getRecipes.Where(x =>
                        x.RecipesIngredients.Select(y => y.IngredientId).Intersect(ingredientsIds).ToList().Count ==
                        ingredientsIds.Count)
                    .ToList();
            }

            if (filters != null)
            {
                filtersIds = filters.Select(filter => filter.Id).ToList();
                getRecipes = getRecipes.Where(x =>
                        x.RecipesFilters.Select(y => y.FilterId).Intersect(filtersIds).ToList().Count == filtersIds.Count)
                    .ToList();
            }

            return getRecipes;
        }

    }
}
