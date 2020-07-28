using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance
{
    public sealed class CollectionRepo : Repository<SavedRecipes>, ICollectionRepo
    {
        public CollectionRepo(TastyBoutique_v2Context context) : base(context) { }

        public async Task<IList<Models.Recipes>> GetAllSavedByIdUser(Guid idUser)
            =>  await (from recipe in context.SavedRecipes where recipe.IdUser == idUser
                select recipe.IdRecipeNavigation).ToListAsync();

        public async Task<IList<Models.Recipes>> GetAllNotificationsByIdUser(Guid idUser)
            =>  await (from recipe in context.SavedRecipes
                where recipe.NeedUpdate && recipe.IdUser == idUser
                select recipe.IdRecipeNavigation).ToListAsync();

        public async Task<SavedRecipes> Get(Guid idUser, Guid idRecipe)
            => await this.context.SavedRecipes.Where(x => x.IdUser == idUser)
                .Where(x => x.IdRecipe == idRecipe)
                .FirstAsync();

        public async Task SetAllByIdRecipe(Guid idRecipe)
        {
            var savedRecipes = await this.context.SavedRecipes.Where(x => x.IdRecipe == idRecipe).ToListAsync();
            foreach (var savedRecipe in savedRecipes)
                savedRecipe.NeedUpdate = true;
        }
    }
}
