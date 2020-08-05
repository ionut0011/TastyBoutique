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
        public async Task Update(Guid IdRecipe)
        {
            Guid IdUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var savedRecipe = await _collectionRepo.Get(IdUser, IdRecipe);
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
