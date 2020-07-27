using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TastyBoutique.Business.Collections.Models;
using TastyBoutique.Business.Recipes.Models.Recipe;

namespace TastyBoutique.Business.Collections.Services.Interfaces
{
    public interface ICollectionService
    {
        Task<SavedRecipeModel> Add(SavedRecipeModel model);
        Task Delete(SavedRecipeModel model);
        public Task<IList<SavedRecipeModel>> GetAllByIdUser(Guid idUser);
    }
}
