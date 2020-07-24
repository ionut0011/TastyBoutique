using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TastyBoutique.Business.Implementations.Models.Recipe;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Business.Recipes.Services.Interfaces;

namespace TastyBoutique.API.Controller
{
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
            
            return Ok(result.Results);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UpsertRecipeModel model, [FromForm] GetPhotoModel photomodel)
        { 
            
            var result = await _recipeService.Add(model, photomodel);
            
            
            return Created(result.Id.ToString(), null);
        }

        [HttpPatch("{recipeid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpsertRecipeModel model, [FromForm] GetPhotoModel pmodel)
        {
            await _recipeService.Update(id, model, pmodel);
            return NoContent();
        }

        [HttpDelete("{recipeid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _recipeService.Delete(id);
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

