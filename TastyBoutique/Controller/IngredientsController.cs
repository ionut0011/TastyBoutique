using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

            return Ok(result.Results);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateIngredientModel model)
        {
            var result = await _ingredientService.Add(model);
            return Ok(result);
        }

        [HttpGet("{ingredientName}")]
        public async Task<IActionResult> Get([FromRoute] String ingredientName)
        {
            var result = await _ingredientService.GetByName(ingredientName);
            return Ok(result);
        }


    }
}
