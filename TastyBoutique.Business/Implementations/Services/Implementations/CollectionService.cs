using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TastyBoutique.Business.Collections.Models;
using TastyBoutique.Business.Collections.Services.Interfaces;
using TastyBoutique.Persistance;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Business.Collections.Services.Implementation
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepo _repository; 
        private readonly IMapper _mapper;

        public CollectionService(ICollectionRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SavedRecipeModel> Add(SavedRecipeModel model)
        {
            var recipe = _mapper.Map<SavedRecipes>(model);
            await _repository.Add(recipe);
            await _repository.SaveChanges();

            return _mapper.Map<SavedRecipeModel>(recipe);
        }

        public async Task Delete(SavedRecipeModel model)
        {
            var recipe = await _repository.Get(model.IdUser, model.IdRecipe);
            _repository.Delete(recipe);
            await _repository.SaveChanges();
        }

        public async Task<IList<SavedRecipeModel>> GetAllByIdUser(Guid idUser)
        {
            var result = await _repository.GetAllByIdUser(idUser);
            return _mapper.Map<IList<SavedRecipeModel>>(result);
        }
    }
}
