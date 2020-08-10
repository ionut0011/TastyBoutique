using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Recipes
{
    public sealed class RecipeRepository : Repository<Models.Recipes>, IRecipeRepository
    {
        public RecipeRepository(TastyBoutiqueContext context) : base(context) { }

        public async new Task<Models.Recipes> GetById(Guid id)
           => await this.context.Recipes
            .Include(r => r.Filters)
            .ThenInclude(r => r.Filter)
            .Include(r => r.Ingredients)
            .ThenInclude(r => r.Ingredient)
            .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<IList<Models.Recipes>> Get(Guid idUser)
            => await this.context.Recipes
            .Where(recipe => recipe.Access || recipe.IdUser == idUser)
            .Include(r => r.Filters)
            .ThenInclude(r => r.Filter)
            .Include(r => r.Ingredients)
            .ThenInclude(r => r.Ingredient)
            .ToListAsync();

        public async Task<RecipeComment> GetRecipeComment(Guid commentId)
            => await this.context.RecipeComments.Where(x => x.Id == commentId).FirstOrDefaultAsync();
        

        public async void DeleteComment(Guid commentId)
        {
            this.context.RecipeComments.Remove(await this.context.RecipeComments.Where(x => x.Id == commentId).FirstAsync());
            // this.context.RecipeComments.Remove(await this.context.RecipeComments
            //    .Where(x => x.Id == commentId).FirstOrDefaultAsync());
        }

        public async Task<IList<Models.Recipes>> GetAllPublic()
            => await this.context.Recipes.
            Where(recipe => recipe.Access)
            .Include(r => r.Filters)
            .ThenInclude(r => r.Filter)
            .Include(r => r.Ingredients)
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
                .Include(r => r.Filters)
                .ThenInclude(r => r.Filter)
                .Include(r => r.Ingredients)
                .ThenInclude(r => r.Ingredient)
                .FirstOrDefaultAsync(recipe => recipe.Id == id);

        public async Task<List<Models.Recipes>> GetRecipiesByQuery(Guid idUser, IList<Models.Ingredients> ingredients, ISpecification<Models.Recipes> spec)
        {
            var getRecipes = await this.context.Recipes.ExeSpec(spec)
                 .Where(recipe => recipe.Access || recipe.IdUser == idUser)
                 .Include(r=>r.Filters)
                 .ThenInclude(r=>r.Filter)
                 .Include(r=>r.Ingredients)
                 .ThenInclude(r=>r.Ingredient)
                 .ToListAsync();

            if (ingredients != null)
            {
                List<Guid> ingredientsIds = ingredients.Select(ingredient => ingredient.Id).ToList();
                getRecipes = getRecipes.Where(x =>
                        x.Ingredients.Select(y => y.IngredientId).Intersect(ingredientsIds).ToList().Count ==
                        ingredientsIds.Count).ToList();
            }

            return getRecipes;
        }


        public async Task<List<Models.Recipes>> GetRecipiesByFilter(Guid idUser, Filters filter, ISpecification<Models.Recipes> spec)
        {
            var getRecipes = await this.context.Recipes.ExeSpec(spec)
                .Where(recipe => (recipe.Access || recipe.IdUser == idUser) && recipe.Filters.Select(f=>f.Filter).Contains(filter))
                .Include(r => r.Filters)
                .ThenInclude(r => r.Filter)
                .Include(r => r.Ingredients)
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
