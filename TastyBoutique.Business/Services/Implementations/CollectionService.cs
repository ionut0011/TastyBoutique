﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _accessor;

        public CollectionService(ICollectionRepo repository, IRecipeRepo recipeRepo, IMapper mapper, IHttpContextAccessor accessor)
        {
            _repository = repository;
            _recipeRepo = recipeRepo;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task Add(SavedRecipeModel model)
        {
            model.IdUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var recipe = _mapper.Map<SavedRecipes>(model);
            await _repository.Add(recipe);
            await _repository.SaveChanges();
        }

        public async Task Delete(Guid recipeId)
        {
            var IdUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var savedRecipe = await _repository.Get(IdUser, recipeId);
            _repository.Delete(savedRecipe);
            await _repository.SaveChanges();
        }

        public async Task Update(SavedRecipeModel model)
        {
            model.IdUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var savedRecipe = await _repository.Get(model.IdUser, model.IdRecipe);
            
            savedRecipe.IdRecipeNavigation = await _recipeRepo.GetById(savedRecipe.IdRecipe);
            savedRecipe.NeedUpdate = false;
            await _repository.SaveChanges();
        }

        public async Task<PaginatedList<RecipeModel>> GetAllByIdUser(SearchModel model)
        {
            Guid idUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
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
