using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Models.RecipeComment;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;

namespace TastyBoutique.API.Controller
{

    [ApiController]
    [Route("api/v1/recipe")]
    public sealed class RecipeCommentController : ControllerBase
    {
        private readonly IRecipeCommentService _commentsService;
        private readonly IHttpContextAccessor _accessor;

        public RecipeCommentController(IRecipeCommentService commentsService, IHttpContextAccessor accessor)
        {
            _commentsService = commentsService;
            _accessor = accessor;
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
            var userId = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var result = await _commentsService.Add(userId, recipeId, model);

            return Ok(result);
        }

        [HttpDelete("comments/{commentId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid commentId)
        {
            var userId = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            await _commentsService.Delete(commentId, userId);

            return NoContent();
        }
    }
}
