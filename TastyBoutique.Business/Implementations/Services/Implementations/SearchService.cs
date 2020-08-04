﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TastyBoutique.Business.Implementations.Models;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;
using TastyBoutique.Persistance.Repositories.Filters;
using TastyBoutique.Business.Recipes.Extensions;

namespace TastyBoutique.Business.Implementations.Services.Implementations
{
    public class SearchService : ISearchService
    {
        private readonly IIngredientsRepo _ingredientsRepo;
        private readonly IFiltersRepo _filtersRepo;
        private readonly IRecipeRepo _recipeRepo;
        private readonly IMapper _mapper;

        public SearchService(IIngredientsRepo ingredientsRepo, IFiltersRepo filtersRepo, IRecipeRepo recipeRepo, IMapper mapper)
        {
            _ingredientsRepo = ingredientsRepo;
            _filtersRepo = filtersRepo;
            _recipeRepo = recipeRepo;
            _mapper = mapper;
        }

        public async Task<IList<Ingredients>> MapIngredients(IList<string> ingredientsList)
        {
            var result = new List<Ingredients>();

            foreach (var ingredient in ingredientsList)
            {
                result.Add(await _ingredientsRepo.GetByName(ingredient));
            }

            return result;
        }

        public async Task<PaginatedList<RecipeModel>> GetRecipiesByQuery(Guid idUser, IList<string> ingredientsList, SearchModel model)
        {
            var spec = model.ToSpecification<Persistance.Models.Recipes>();

            IList<Ingredients> ingredients = null;

            if (ingredientsList.Count != 0)
                ingredients = await MapIngredients(ingredientsList);

            var result = await _recipeRepo.GetRecipiesByQuery(idUser, ingredients, spec);

            return new PaginatedList<RecipeModel>(
                model.PageIndex,
                result.Count,
                result.Count,
                _mapper.Map<IList<RecipeModel>>(result));
        }

        public  async Task<PaginatedList<RecipeModel>> GetRecipiesByFilter(Guid idUser, string filter, SearchModel model)
        {
            var spec = model.ToSpecification<Persistance.Models.Recipes>();
            var f = await _filtersRepo.GetByName(filter);

            var result = await _recipeRepo.GetRecipiesByFilter(idUser, f, spec);

            return new PaginatedList<RecipeModel>(
                model.PageIndex,
                result.Count,
                result.Count,
                _mapper.Map<IList<RecipeModel>>(result));
        }

    }
}
