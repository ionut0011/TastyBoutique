using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TastyBoutique.Business.Implementations.Models;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;
using TastyBoutique.Persistance.Repositories.Filters;

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

        public async Task<IList<Filters>> MapFilters(IList<string> filtersList)
        {
            var result = new List<Filters>();

            foreach (var filter in filtersList)
            {
                result.Add(await _filtersRepo.GetByName(filter));
            }

            return result;
        }

        public async Task<PaginatedList<RecipeModel>> GetRecipiesByQuery(RecipeSearchModel query, SearchModel model)
        {
            IList<Ingredients> ingredients = null;
            IList<Filters> filters = null;

            if (query.IngredientsList != null)
                ingredients = await this.MapIngredients(query.IngredientsList);
            if (query.FiltersList != null)
                filters = await this.MapFilters(query.FiltersList);

            var result = await _recipeRepo.GetRecipiesByQuery(ingredients, filters);

            return new PaginatedList<RecipeModel>(
                model.PageIndex,
                result.Count,
                result.Count,
                _mapper.Map<IList<RecipeModel>>(result));
        }

    }
}
