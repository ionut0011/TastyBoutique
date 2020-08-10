using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Models.Recipe;

namespace TastyBoutique.API.Controller
{
    [Authorize]
    [Route("api/v1/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _accessor;

        public NotificationsController(INotificationService notificationService, IHttpContextAccessor accessor)
        {
            _notificationService = notificationService;
            _accessor = accessor;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> GetAllByIdUser()
        {
            Guid idUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var result = await _notificationService.GetAllByIdUser(idUser);
            return Ok(result.Results);
        }

        [Microsoft.AspNetCore.Mvc.HttpPatch("{recipeId}")]
        public async Task<IActionResult> Update([FromRoute] Guid recipeId)
        {
            var model = new SavedRecipeModel();
            model.IdRecipe = recipeId;
            model.IdUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);

            await _notificationService.Update(model);
            return NoContent();
        }

    }
}
