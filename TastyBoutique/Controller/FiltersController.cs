using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TastyBoutique.Business.Implementations.Models.Filter;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Business.Recipes.Services.Interfaces;

namespace TastyBoutique.API.Controller
{
    [ApiController]
    [Route("api/v1/filter")]
    public sealed class FiltersController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly IFilterService _filterService;

        public FiltersController(IFilterService filterService)
        {
            _filterService = filterService;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchModel model)
        {
            var result = await _filterService.Get(model);

            return Ok(result.Results);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateFilterModel model)
        {
            var result = await _filterService.Add(model);
            return Ok(result);
        }
        [HttpGet("{filterName}")]
        public async Task<IActionResult> Get([FromRoute] string filterName)
        {
            var result = await _filterService.GetFilterByName(filterName);
            return Ok(result);
        }

    }
}
