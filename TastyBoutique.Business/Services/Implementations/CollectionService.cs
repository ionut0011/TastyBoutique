using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TastyBoutique.Business.Extensions;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;
using TastyBoutique.Business.Services.Interfaces;
using TastyBoutique.Persistance;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Business.Services.Implementation
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _repository; 
        private readonly IMapper _mapper;

        public CollectionService(ICollectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Add(SavedRecipeModel model)
        {
            var savedRecipes = await _repository.Get(model.IdUser, model.IdRecipe);
            if (savedRecipes == null)
            {
                var recipe = _mapper.Map<SavedRecipes>(model);
                await _repository.Add(recipe);
                await _repository.SaveChanges();
            }
        }

        public async Task Delete(SavedRecipeModel model)
        {
            var savedRecipe = await _repository.Get(model.IdUser, model.IdRecipe);
            _repository.Delete(savedRecipe);
            await _repository.SaveChanges();
        }

        //public async Task Update(SavedRecipeModel model)
        //{
        //    var savedRecipe = await _repository.Get(model.IdUser, model.IdRecipe);
            
        //    savedRecipe.IdRecipeNavigation = await _recipeRepo.GetById(savedRecipe.IdRecipe);
        //    savedRecipe.NeedUpdate = false;

        //    await _repository.SaveChanges();
        //}

        public async Task<PaginatedList<TotalRecipeModel>> GetAllByIdUser(Guid idUser, SearchModel model)
        {
           
            var spec = model.ToSpecification<Persistance.Models.Recipes>();
            var result = await _repository.GetAllSavedByIdUser(idUser, spec);

            var count = await _repository.CountAsync();

            return new PaginatedList<TotalRecipeModel>(
                model.PageIndex,
                result.Count,
                count,
                _mapper.Map<IList<TotalRecipeModel>>(result));
        }
    }
}
