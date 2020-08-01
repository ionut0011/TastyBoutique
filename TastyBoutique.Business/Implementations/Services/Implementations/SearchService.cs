using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;

namespace TastyBoutique.Business.Implementations.Services.Implementations
{
    public class SearchService : ISearchService
    {
        private readonly IIngredientsRepo _ingredientsRepo;
        private readonly IRecipeRepo _recipeRepo;
        private readonly IMapper _mapper;

        public SearchService(IIngredientsRepo ingredientsRepo, IRecipeRepo recipeRepo, IMapper mapper)
        {
            _ingredientsRepo = ingredientsRepo;
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

        public async Task<PaginatedList<RecipeModel>> GetRecipiesByIngredients(IList<string> ingredientsList, SearchModel model)
        {
            var ingredients = await this.MapIngredients(ingredientsList);

            var result = await _recipeRepo.GetRecipiesByIngredients(ingredients);

            return new PaginatedList<RecipeModel>(
                model.PageIndex,
                result.Count,
                result.Count,
                _mapper.Map<IList<RecipeModel>>(result));
        }

    }
}
