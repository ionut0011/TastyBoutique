using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance
{
    public sealed class CollectionRepo : Repository<SavedRecipes>, ICollectionRepo
    {
        public CollectionRepo(TastyBoutiqueContext context) : base(context) { }

        public async Task<IList<Models.Recipes>> GetAllSavedByIdUser(Guid idUser, ISpecification<Models.Recipes> spec)
            =>  await (from recipe in context.SavedRecipes where recipe.IdUser == idUser
                select recipe.IdRecipeNavigation).ExeSpec(spec)
                .Include(r => r.RecipesFilters)
                .ThenInclude(r => r.Filter)
                .Include(r => r.RecipesIngredients)
                .ThenInclude(r => r.Ingredient)
                .ToListAsync();

        public async Task<IList<Models.Recipes>> GetAllNotificationsByIdUser(Guid idUser)
            =>  await context.SavedRecipes
                .Where(recipe=>recipe.NeedUpdate && recipe.IdUser == idUser)
                .Include(r => r.IdRecipeNavigation.RecipesFilters)
                .ThenInclude(r => r.Filter)
                .Include(r => r.IdRecipeNavigation.RecipesIngredients)
                .ThenInclude(r => r.Ingredient)
                .Select(r=>r.IdRecipeNavigation)
                .ToListAsync();

        public async Task<SavedRecipes> Get(Guid idUser, Guid idRecipe)
            => await this.context.SavedRecipes.Where(x => x.IdUser == idUser)
                .Where(x => x.IdRecipe == idRecipe)
                .Include(x=>x.IdRecipeNavigation)
                .FirstAsync();

        public async Task SetAllByIdRecipe(Guid idRecipe)
        {
            var savedRecipes = await this.context.SavedRecipes.Where(x => x.IdRecipe == idRecipe).ToListAsync();
            foreach (var savedRecipe in savedRecipes)
                savedRecipe.NeedUpdate = true;
        }

        public async Task<int> CountAsync()
            => await this.context.Recipes.CountAsync();
    }
}
