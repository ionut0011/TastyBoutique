using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;
using TastyBoutique.Business.Recipes.Services.Interfaces;

namespace TastyBoutique.API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/v1/recipe")]
    public sealed class RecipeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchModel model)
        {
            var result = await _recipeService.Get(model);
            
            return Ok(result);
        }

        [HttpGet("{recipeId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid recipeId)
        {
            var result = await _recipeService.GetById(recipeId);

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([Microsoft.AspNetCore.Mvc.FromBody] UpsertRecipeModel model)
        { 
            
            var result = await _recipeService.Add(model);

            return Ok(result);
        }

        [HttpPatch("{recipeId}")]
        public async Task<IActionResult> Update([FromRoute] Guid recipeId, [FromBody] UpsertRecipeModel model)
        {
            await _recipeService.Update(recipeId, model);
            return NoContent();
        }

        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid recipeId)
        {
            await _recipeService.Delete(recipeId);
            return NoContent();
        }
        [HttpGet("{recipeId}/ingredients")]
        public async Task<IActionResult> GetIngredients([FromRoute] Guid recipeId)
        {
            var result = await _recipeService.GetIngredientsByRecipeId(recipeId);

            return Ok(result.Results);
        }

        [HttpGet("{recipeId}/filters")]
        public async Task<IActionResult> GetFilters([FromRoute] Guid recipeId)
        {
            var result = await _recipeService.GetFiltersByRecipeId(recipeId);

            return Ok(result.Results);
        }
    }
}

