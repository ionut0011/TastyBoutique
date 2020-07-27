using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Collections.Models;
using TastyBoutique.Business.Collections.Services.Interfaces;
using TastyBoutique.Business.Recipes.Models.Recipe;

namespace TastyBoutique.API.Controller
{
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

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] SavedRecipeModel model)
        {
            await _collectionService.Delete(model);
            return NoContent();
        }

        //[HttpPatch]
        //public async Task<IActionResult> Update([FromBody] SavedRecipeModel model)
        //{
        //    await _collectionService.Update(model);
        //    return NoContent();
        //}


        [HttpGet("{idUser}")]
        public async Task<IActionResult> GetAllByIdUser([FromRoute] Guid idUser)
        {
            var result = await _collectionService.GetAllByIdUser(idUser);
            return Ok(result);
        }

    }
}
