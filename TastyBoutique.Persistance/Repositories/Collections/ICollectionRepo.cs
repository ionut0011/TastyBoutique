using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LinqBuilder.Core;

namespace TastyBoutique.Persistance
{
    public interface ICollectionRepo : IRepository<Models.SavedRecipes>
    {
        Task<IList<Models.SavedRecipes>> GetAllByIdUser(Guid idUser);
        public Task<Models.SavedRecipes> Get(Guid idUser, Guid idRecipe);
        Task<int> CountAsync();

    }
}
