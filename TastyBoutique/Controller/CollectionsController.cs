using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Collections.Models;
using TastyBoutique.Business.Collections.Services.Interfaces;
using TastyBoutique.Business.Recipes.Models.Recipe;

namespace TastyBoutique.API.Controller
{
    [Authorize]
    [Route("api/v1/collections")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionsController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SavedRecipeModel model)
        {
            await _collectionService.Add(model);
            return NoContent();
        }

        [HttpDelete ("{idRecipe}")]

        public async Task<IActionResult> Delete([FromRoute] Guid idRecipe)
        {
            await _collectionService.Delete(idRecipe);
            return NoContent();
        }

        //[HttpPatch]
        //public async Task<IActionResult> Update([FromBody] SavedRecipeModel model)
        //{
        //    await _collectionService.Update(model);
        //    return NoContent();
        //}


        [HttpGet]
        public async Task<IActionResult> GetAllByIdUser([FromQuery]SearchModel model)
        {
            var result = await _collectionService.GetAllByIdUser(model);
            return Ok(result.Results);
        }

    }
}
