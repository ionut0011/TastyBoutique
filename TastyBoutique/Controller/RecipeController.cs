using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TastyBoutique.Business.Recipes.Models.Recipe;
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

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchModel model)
        {
            var result = await _recipeService.Get(model);
            
            return Ok(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{recipeId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid recipeId)
        {
            var result = await _recipeService.GetById(recipeId);

            return Ok(result);
        }
        
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> Add([Microsoft.AspNetCore.Mvc.FromBody] UpsertRecipeModel model)
        { 
            
            var result = await _recipeService.Add(model);
            
            return Created(result.Id.ToString(), null);
        }

        [Microsoft.AspNetCore.Mvc.HttpPatch("{recipeId}")]
        public async Task<IActionResult> Update([FromRoute] Guid recipeId, [FromBody] UpsertRecipeModel model)
        {
            await _recipeService.Update(recipeId, model);
            return NoContent();
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("{recipeId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid recipeId)
        {
            await _recipeService.Delete(recipeId);
            return NoContent();
        }
        [Microsoft.AspNetCore.Mvc.HttpGet("{recipeId}/ingredients")]
        public async Task<IActionResult> GetIngredients([FromRoute] Guid recipeId)
        {
            var result = await _recipeService.GetIngredientsByRecipeId(recipeId);

            return Ok(result.Results);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{recipeId}/filters")]
        public async Task<IActionResult> GetFilters([FromRoute] Guid recipeId)
        {
            var result = await _recipeService.GetFiltersByRecipeId(recipeId);

            return Ok(result.Results);
        }
    }
}

