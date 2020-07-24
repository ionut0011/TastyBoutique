using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Implementations.Models.Filter;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Business.Recipes.Services.Interfaces;

namespace TastyBoutique.API.Controller
{
    [ApiController]
    [Route("api/v1/filter")]
    public sealed class FiltersController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly IFilterService _filterService;

        public FiltersController(IFilterService filterService)
        {
            _filterService = filterService;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchModel model)
        {
            var result = await _filterService.Get(model);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateFilterModel model)
        {
            var result = await _filterService.Add(model);
            return Created(result.Id.ToString(), null);
        }

        [HttpGet("{recipeId}")]
        public async Task<IActionResult> GetFilters([FromRoute] Guid recipeId)
        {
            var result = await _filterService.GetFiltersByRecipeId(recipeId);

            return Ok(result);
        }
    }
}
