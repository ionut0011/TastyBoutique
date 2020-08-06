using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _accessor;
        public SearchController(ISearchService searchService, IHttpContextAccessor accessor)
        {
            _searchService = searchService;
            _accessor = accessor;
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] IList<string> query, [FromQuery] SearchModel model)
        {
            var idUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var result = await _searchService.GetRecipiesByQuery(idUser, query, model);
            return Ok(result.Results);
        }

        [HttpGet("{filter}")]
        public async Task<IActionResult> Filter([FromRoute] string filter, [FromQuery] SearchModel model)
        {
            var idUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var result = await _searchService.GetRecipiesByFilter(idUser, filter, model);
            return Ok(result.Results);
        }

    }
}
