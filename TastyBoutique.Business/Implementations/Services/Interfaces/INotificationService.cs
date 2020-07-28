using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Collections.Models;
using TastyBoutique.Business.Recipes.Models.Recipe;

namespace TastyBoutique.Business.Implementations.Services.Interfaces
{
    public interface INotificationService
    {
        public Task<PaginatedList<RecipeModel>> GetAllByIdUser(Guid idUser);

        public Task Update(SavedRecipeModel model);
        public Task SetAllByIdRecipe(Guid idRecipe);
    }
}
