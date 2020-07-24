using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqBuilder.Core;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance
{
    public sealed class CollectionRepo : Repository<Models.SavedRecipes>, ICollectionRepo
    {
        public CollectionRepo(TastyBoutique_v2Context context) : base(context) { }

        public async Task<IList<Models.SavedRecipes>> GetAllByIdUser(Guid IdUser)
            =>  await this.context.SavedRecipes.Where(x => x.IdUser == IdUser).ToListAsync();

        public async Task<Models.SavedRecipes> Get(Guid idUser, Guid idRecipe)
            => await this.context.SavedRecipes.Where(x => x.IdUser == idUser)
                    .Where(x => x.IdRecipe == idRecipe)
                    .FirstAsync();
        public async Task<int> CountAsync()
            => await this.context.Recipes.CountAsync();
    }
}
