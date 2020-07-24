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
    [Route("api/v1/recipe/comments")]
    public sealed class RecipeCommentController : ControllerBase
    {
        private readonly IRecipeCommentService _commentsService;

        public RecipeCommentController(IRecipeCommentService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid IdRecipe)
        {
            var result = await _commentsService.Get(IdRecipe);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute] Guid IdRecipe, [System.Web.Http.FromBody] CreateRecipeCommentModel model)
        {
            var result = await _commentsService.Add(IdRecipe, model);

            return Created(result.IdRecipe.ToString(),null);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid IdRecipe, [FromRoute] Guid commentId)
        {
            await _commentsService.Delete(IdRecipe, commentId);

            return NoContent();
        }
    }
}
