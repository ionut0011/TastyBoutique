using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromQuery] UpsertRecipeModel model, [FromForm] GetPhotoModel pmodel)
        { 
            var result = await _recipeService.Add(model,pmodel);
            return Created(result.Id.ToString(), null);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpsertRecipeModel model, [FromForm] GetPhotoModel pmodel)
        {
            await _recipeService.Update(id, model, pmodel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _recipeService.Delete(id);
            return NoContent();
        }
    }
}
