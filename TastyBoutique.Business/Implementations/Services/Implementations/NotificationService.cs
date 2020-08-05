using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TastyBoutique.Business.Collections.Models;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Persistance;
using TastyBoutique.Persistance.Recipes;

namespace TastyBoutique.Business.Implementations.Services.Implementations
{
    public sealed class NotificationService : INotificationService
    {
        private readonly ICollectionRepo _collectionRepo;
        private readonly IMapper _mapper;
        //private readonly IRecipeRepo _recipeRepo;
        private readonly IHttpContextAccessor _accessor;

        public NotificationService(ICollectionRepo collectionRepo, IMapper mapper, IHttpContextAccessor accessor)
        {
            _collectionRepo = collectionRepo;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task<PaginatedList<RecipeModel>> GetAllByIdUser(Guid idUser)
        {
            var savedRecipes = await _collectionRepo.GetAllNotificationsByIdUser(idUser);
            return new PaginatedList<RecipeModel>(
                1,
                savedRecipes.Count,
                savedRecipes.Count,
                _mapper.Map<IList<RecipeModel>>(savedRecipes));
        }

        public async Task Update(SavedRecipeModel model)
        {
            model.IdUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var savedRecipe = await _collectionRepo.Get(model.IdUser, model.IdRecipe);
            savedRecipe.NeedUpdate = false;
            await _collectionRepo.SaveChanges();
        }

        public async Task SetAllByIdRecipe(Guid idRecipe)
        {
            await _collectionRepo.SetAllByIdRecipe(idRecipe);
            await _collectionRepo.SaveChanges();
        }
    }
}
