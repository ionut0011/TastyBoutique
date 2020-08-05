using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;
using TastyBoutique.Persistance;

namespace TastyBoutique.Business.Services.Implementations
{
    public sealed class NotificationService : INotificationService
    {
        private readonly ICollectionRepo _collectionRepo;
        private readonly IMapper _mapper;
        //private readonly IRecipeRepo _recipeRepo;

        public NotificationService(ICollectionRepo collectionRepo, IMapper mapper)
        {
            _collectionRepo = collectionRepo;
            _mapper = mapper;
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
