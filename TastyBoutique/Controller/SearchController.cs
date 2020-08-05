using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Models.Shared;

namespace TastyBoutique.API.Controller
{
    [Route("api/v1/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] Guid idUser, [FromQuery] IList<string> query, [FromQuery] SearchModel model)
        {
            var result = await _searchService.GetRecipiesByQuery(idUser, query, model);
            return Ok(result.Results);
        }

        [HttpGet("{filter}")]
        public async Task<IActionResult> Filter([FromRoute] string filter, [FromQuery] Guid idUser, [FromQuery] SearchModel model)
        {
            var result = await _searchService.GetRecipiesByFilter(idUser, filter, model);
            return Ok(result.Results);
        }

    }
}
