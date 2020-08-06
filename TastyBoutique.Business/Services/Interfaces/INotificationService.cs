using System;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;

namespace TastyBoutique.Business.Implementations.Services.Interfaces
{
    public interface INotificationService
    {
        public Task<PaginatedList<TotalRecipeModel>> GetAllByIdUser(Guid idUser);

        public Task Update(Guid IdRecipe);
        public Task SetAllByIdRecipe(Guid idRecipe);


    }
}
