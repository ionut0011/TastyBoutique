using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TastyBoutique.Business.Collections.Services.Interfaces;
using TastyBoutique.Business.Extensions;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;
using TastyBoutique.Persistance;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;

namespace TastyBoutique.Business.Services.Implementation
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepo _repository; 
        private readonly IMapper _mapper;
        private readonly IRecipeRepo _recipeRepo;

        public CollectionService(ICollectionRepo repository, IRecipeRepo recipeRepo, IMapper mapper)
        {
            _repository = repository;
            _recipeRepo = recipeRepo;
            _mapper = mapper;
        }

        public async Task Add(SavedRecipeModel model)
        {
            var recipe = _mapper.Map<SavedRecipes>(model);
            await _repository.Add(recipe);
            await _repository.SaveChanges();
        }

        public async Task Delete(SavedRecipeModel model)
        {
            var savedRecipe = await _repository.Get(model.IdUser, model.IdRecipe);
            _repository.Delete(savedRecipe);
            await _repository.SaveChanges();
        }

        public async Task Update(SavedRecipeModel model)
        {
            var savedRecipe = await _repository.Get(model.IdUser, model.IdRecipe);
            savedRecipe.IdRecipeNavigation = await _recipeRepo.GetById(savedRecipe.IdRecipe);
            savedRecipe.NeedUpdate = false;
            await _repository.SaveChanges();
        }

        public async Task<PaginatedList<RecipeModel>> GetAllByIdUser(Guid idUser, SearchModel model)
        {
            var spec = model.ToSpecification<Persistance.Models.Recipes>();
            var result = await _repository.GetAllSavedByIdUser(idUser, spec);

            var count = await _repository.CountAsync();

            return new PaginatedList<RecipeModel>(
                model.PageIndex,
                result.Count,
                count,
                _mapper.Map<IList<RecipeModel>>(result));
        }
    }
}
