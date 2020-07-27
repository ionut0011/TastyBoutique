using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Collections.Models;
using TastyBoutique.Business.Collections.Services.Interfaces;
using TastyBoutique.Business.Implementations.Services.Interfaces;

namespace TastyBoutique.API.Controller
{
    [Route("api/v1/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{idUser}")]
        public async Task<IActionResult> GetAllByIdUser([FromRoute] Guid idUser)
        {
            var result = await _notificationService.GetAllByIdUser(idUser);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] SavedRecipeModel model)
        {
            await _notificationService.Update(model);
            return NoContent();
        }

    }
}
