using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqBuilder.Core;

namespace TastyBoutique.Persistance
{
    public interface ICollectionRepository : IRepository<Models.SavedRecipes>
    {
        Task<IList<Models.Recipes>> GetAllSavedByIdUser(Guid idUser, ISpecification<Models.Recipes> spec);
        Task<IList<Models.Recipes>> GetAllNotificationsByIdUser(Guid idUser);
        Task<Models.SavedRecipes> Get(Guid idUser, Guid idRecipe);
        Task SetAllByIdRecipe(Guid idRecipe);
        Task<int> CountAsync();
    }
}
