using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Implementations.Services.Interfaces;

namespace TastyBoutique.API.Controller
{
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

        [HttpGet]
        public async Task<IActionResult> GetAllByIdUser()
        {
            Guid idUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var result = await _notificationService.GetAllByIdUser(idUser);
            return Ok(result.Results);
        }

        [HttpPatch("{idRecipe}")]
        public async Task<IActionResult> Update([FromRoute] Guid idRecipe)
        {
            await _notificationService.Update(idRecipe);
            return NoContent();
        }

    }
}
