using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.Business.Models.Shared;
using TastyBoutique.Business.Services.Interfaces;

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
            return Created(result.Id.ToString(), null);
        }

        //[HttpGet("{filterName}")]
        //public async Task<IActionResult> Get([FromRoute] string filterName)
        //{
        //    var result = await _filterService.GetFilterByName(filterName);
        //    return Ok(result);
        //}

    }
}
