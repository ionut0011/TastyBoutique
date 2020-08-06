using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;
using TastyBoutique.Business.Services.Interfaces;

namespace TastyBoutique.API.Controller
{
    [Authorize]
    [Route("api/v1/collections")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;
        private readonly IHttpContextAccessor _accessor;
        public CollectionsController(ICollectionService collectionService, IHttpContextAccessor accessor)
        {
            _collectionService = collectionService;
            _accessor = accessor;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SavedRecipeModel model)
        {
            model.IdUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            await _collectionService.Add(model);
            return NoContent();
        }

        [HttpDelete ("{idRecipe}")]

        public async Task<IActionResult> Delete([FromRoute] Guid idRecipe)
        {
            var model = new SavedRecipeModel();
            model.IdRecipe = idRecipe;
            model.IdUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            await _collectionService.Delete(model);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByIdUser([FromQuery]SearchModel model)
        {
            Guid idUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var result = await _collectionService.GetAllByIdUser(idUser, model);
            return Ok(result.Results);
        }

    }
}
