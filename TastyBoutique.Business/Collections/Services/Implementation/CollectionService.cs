using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TastyBoutique.Business.Collections.Models;
using TastyBoutique.Business.Collections.Services.Interfaces;
using TastyBoutique.Business.Recipes.Models.Recipe;
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
            var recipe = _mapper.Map<SavedRecipes>(model);
            _repository.Delete(recipe);
            await _repository.SaveChanges();
        }
    }
}
