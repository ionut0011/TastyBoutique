using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Recipes
{
    public sealed class RecipeRepo : Repository<Models.Recipes>, IRecipeRepo
    {
        public RecipeRepo(TastyBoutiqueContext context) : base(context) { }
        private int count = 0;
        public async Task<IList<Models.Recipes>> Get(Guid idUser, ISpecification<Models.Recipes> spec)
            => await this.context.Recipes.ExeSpec(spec).Where(recipe=>recipe.Access || recipe.IdUser == idUser).ToListAsync();

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

        public async Task<RecipeType> GetRecipeTypeById(Guid id)
            => await this.context.RecipeType
                .FirstOrDefaultAsync(recipeType => recipeType.RecipeId == id);

        public async Task<List<Models.Recipes>> GetRecipiesByQuery(Guid idUser, IList<Models.Ingredients> ingredients, ISpecification<Models.Recipes> spec)
        {
            var getRecipes = this.context.Recipes.ExeSpec(spec)
                .Where(recipe => recipe.Access || recipe.IdUser == idUser);

            if (ingredients != null)
            {
                var ingredientsIds = ingredients.Select(ingredient => ingredient.Id);
                getRecipes = getRecipes
                    .Where(x => x.RecipesIngredients
                        .Select(y => y.IngredientId)
                        .All(id=> ingredientsIds.Contains(id)));
            }

            return await getRecipes.Include(r => r.RecipesIngredients).ToListAsync();
        }


        public async Task<List<Models.Recipes>> GetRecipiesByFilter(Guid idUser, Filters filter, ISpecification<Models.Recipes> spec)
        {
            var getRecipes = await this.context.Recipes.ExeSpec(spec)
                .Include(r => r.RecipesFilters)
                .Where(recipe => (recipe.Access || recipe.IdUser == idUser) && recipe.RecipesFilters.Select(f=>f.Filter).Contains(filter)).ToListAsync();

            return getRecipes;
        }
    }
}
