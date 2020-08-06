using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Recipes
{
    public sealed class RecipeRepo : Repository<Models.Recipes>, IRecipeRepo
    {
        public RecipeRepo(TastyBoutiqueContext context) : base(context) { }
        public async Task<IList<Models.Recipes>> Get(Guid idUser)
            => await this.context.Recipes
            .Where(recipe => recipe.Access || recipe.IdUser == idUser)
            .Include(r => r.RecipesFilters)
            .ThenInclude(r => r.Filter)
            .Include(r => r.RecipesIngredients)
            .ThenInclude(r => r.Ingredient)
            .ToListAsync();

        public async Task<Models.Recipes> GetRecipeById(Guid recipeId)
            => await this.context.Recipes
                .Where(x => x.Id == recipeId)
                .Include(r => r.RecipesFilters)
                .ThenInclude(r => r.Filter)
                .Include(r => r.RecipesIngredients)
                .ThenInclude(r => r.Ingredient)
                .FirstOrDefaultAsync();
        public async Task<IList<Models.Recipes>> GetAllPublic()
            => await this.context.Recipes.
            Where(recipe => recipe.Access)
            .Include(r => r.RecipesFilters)
            .ThenInclude(r => r.Filter)
            .Include(r => r.RecipesIngredients)
            .ThenInclude(r => r.Ingredient).ToListAsync();

        public async Task<int> CountAsync()
            => await this.context.Recipes.CountAsync();

        public async Task<IList<RecipesFilters>> GetFiltersByRecipeId(Guid id)
            => await this.context.RecipesFilters
                .Include(i => i.Filter)
                .Where(i => i.RecipeId == id)
                .ToListAsync();

        public async Task<IList<RecipesIngredients>> GetIngredientsByRecipeId(Guid id)
            => await this.context.RecipesIngredients
                .Include(i => i.Ingredient)
                .Where(i => i.RecipeId == id)
                .ToListAsync();
   
        public async Task<Models.Recipes> GetByIdWithComments(Guid id)
          => await this.context.Recipes
                .Include(recipe => recipe.RecipeComment)
                .Include(r => r.RecipesFilters)
                .ThenInclude(r => r.Filter)
                .Include(r => r.RecipesIngredients)
                .ThenInclude(r => r.Ingredient)
                .FirstOrDefaultAsync(recipe => recipe.Id == id);

        public async Task<List<Models.Recipes>> GetRecipiesByQuery(Guid idUser, IList<Models.Ingredients> ingredients, ISpecification<Models.Recipes> spec)
        {
            var getRecipes = await this.context.Recipes.ExeSpec(spec)
                 .Where(recipe => recipe.Access || recipe.IdUser == idUser)
                 .Include(r=>r.RecipesFilters)
                 .ThenInclude(r=>r.Filter)
                 .Include(r=>r.RecipesIngredients)
                 .ThenInclude(r=>r.Ingredient)
                 .ToListAsync();

            if (ingredients != null)
            {
                List<Guid> ingredientsIds = ingredients.Select(ingredient => ingredient.Id).ToList();
                getRecipes = getRecipes.Where(x =>
                        x.RecipesIngredients.Select(y => y.IngredientId).Intersect(ingredientsIds).ToList().Count ==
                        ingredientsIds.Count).ToList();
            }

            return getRecipes;
        }


        public async Task<List<Models.Recipes>> GetRecipiesByFilter(Guid idUser, Filters filter, ISpecification<Models.Recipes> spec)
        {
            var getRecipes = await this.context.Recipes.ExeSpec(spec)
                .Where(recipe => (recipe.Access || recipe.IdUser == idUser) && recipe.RecipesFilters.Select(f=>f.Filter).Contains(filter))
                .Include(r => r.RecipesFilters)
                .ThenInclude(r => r.Filter)
                .Include(r => r.RecipesIngredients)
                .ThenInclude(r => r.Ingredient)
                .ToListAsync();

            return getRecipes;
        }

        //public async Task<List<RecipeComment>> GetCommentsReview(Guid idRecipe)
        //    => await this.context.RecipeComments
        //        .Where(x => x.IdRecipe == idRecipe)
        //        .ToListAsync();

      
    }
}
