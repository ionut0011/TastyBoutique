using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Business.Recipes.Services.Implementations;
using TastyBoutique.Business.Recipes.Services.Interfaces;

namespace TastyBoutique.API.Controller
{

    [ApiController]
    [Route("api/v1/ingredient")]

    public sealed class IngredientsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchModel model)
        {
            var result = await _ingredientService.Get(model);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateIngredientModel model)
        {
            var result = await _ingredientService.Add(model);
            return Created(result.Id.ToString(), null);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetId([FromRoute] string name)
        {
            var result = await _ingredientService.Get(new SearchModel());
            foreach (var res in result.Results)
            {
                if(res.Name.ToUpper().Equals(name.ToUpper()))
                    return Ok(res.Id);
            }
            return NoContent();
        }

    }
}
