using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;
using TastyBoutique.Persistance;

namespace TastyBoutique.Business.Services.Implementations
{
    public sealed class NotificationService : INotificationService
    {
        private readonly ICollectionRepository _collectionRepo;
        private readonly IMapper _mapper;
        //private readonly IRecipeRepo _recipeRepo;
        

        public NotificationService(ICollectionRepository collectionRepo, IMapper mapper)
        {
            _collectionRepo = collectionRepo;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TotalRecipeModel>> GetAllByIdUser(Guid idUser)
        // returneaza retetele care au fost schimbate
        {
            var savedRecipes = await _collectionRepo.GetAllNotificationsByIdUser(idUser);
            return new PaginatedList<TotalRecipeModel>(
                1,
                savedRecipes.Count,
                savedRecipes.Count,
                _mapper.Map<IList<TotalRecipeModel>>(savedRecipes));
        }

        // apelata cand userul apasa click pe notificare
        public async Task Update(SavedRecipeModel model)
        {
            var savedRecipe = await _collectionRepo.Get(model.IdUser, model.IdRecipe);
            savedRecipe.NeedUpdate = false;
            await _collectionRepo.SaveChanges();
        }

        // cand fac update, trebuie apelata functia asta pt a pune "needupdate" in savedrecipes pe true
        public async Task SetAllByIdRecipe(Guid idRecipe)
        {
            await _collectionRepo.SetAllByIdRecipe(idRecipe);
            await _collectionRepo.SaveChanges();
        }
    }
}
