using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Recipes.Models.RecipeComment;
using TastyBoutique.Business.Recipes.Services.Interfaces;


using Microsoft.AspNetCore.Authorization;


using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;

namespace TastyBoutique.API.Controller
{
    
    [ApiController]
    [Route("api/v1/recipe")]
    public sealed class RecipeCommentController : ControllerBase
    {
        private readonly IRecipeCommentService _commentsService;

        public RecipeCommentController(IRecipeCommentService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet("{recipeId}/comments")]
        public async Task<IActionResult> Get([FromRoute] Guid recipeId)
        {
            var result = await _commentsService.Get(recipeId);

            return Ok(result);
        }

        [HttpPost("{recipeId}/comments")]

        public async Task<IActionResult> Add([FromRoute] Guid recipeId, [FromBody] CreateRecipeCommentModel model)
        {
            var result = await _commentsService.Add(recipeId, model);

            return Created(result.IdRecipe.ToString(),null);
        }

        [HttpDelete("comments/{commentId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid IdRecipe, [FromRoute] Guid commentId)
        {
            await _commentsService.Delete(IdRecipe, commentId);

            return NoContent();
        }
    }
}
