using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Implementations.Services.Implementations;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Persistance.Ingredients;

namespace TastyBoutique.API.Controller
{
    [Route("api/v1/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] IList<string> ingredientsList, [FromQuery]SearchModel model)
        {
            var result = await _searchService.GetRecipiesByIngredients(ingredientsList, model);
            return Ok(result.Results);
        }
    }
}
