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
            => await this.context.Ingredients.Where(i => i.Name.Equals(Name)).FirstOrDefaultAsync();

        public async Task<Models.Recipes> GetByIdWithComments(Guid id)
          => await this.context.Recipes
              .Include(recipe => recipe.RecipeComment)
              .FirstOrDefaultAsync(recipe => recipe.Id == id);

        public async Task<Models.RecipeType> GetRecipeTypeById(Guid id)
            => await this.context.RecipeType
                .FirstOrDefaultAsync(recipeType => recipeType.RecipeId == id);

        public async Task<List<Models.Recipes>> GetRecipiesByIngredients(IList<Models.Ingredients> ingredients)
        {
            var ingredientsId = ingredients.Select(ingredient => ingredient.Id).ToList();

            //var result = context.Recipes
            //    .Include(recipe => recipe.RecipesIngredients)
            //    .Where(recipe => recipe.RecipesIngredients.All(ingredient => ingredientsId.Contains(ingredient.IngredientId)))
            //    .ToListAsync();
            var result = context.Recipes
                .Include(recipe => recipe.RecipesIngredients)
                .Where(x=> ingredientsId.All(x.RecipesIngredients.Select(ri=>ri.IngredientId).Contains))
                .ToListAsync();

            return await result;
        }

    }
}
