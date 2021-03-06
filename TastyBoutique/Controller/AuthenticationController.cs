﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TastyBoutique.Business.Identity.Models;
using TastyBoutique.Business.Identity.Services.Interfaces;

namespace TastyBoutique.API.Controller
{
    [Route("api/v1/auth")]
    [ApiController]
    public sealed class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest model)
        {
            var result = await _authenticationService.Authenticate(model);
            if (result == null)
            {
                return BadRequest("Incorrect username or password");
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var result = await _authenticationService.Register(model);
            if (result == null)
            {
                return BadRequest("User exists or the password doesn't meet the requirements");
            }
            var temp = result.Id.ToString();
            return Created(result.Id.ToString(), null);
        }

        [HttpPost("recover")]
        public async Task<IActionResult> ForgotPassword([FromBody] UserNewPasswordModel model)
        {
            var result = await _authenticationService.ForgotPassword(model);
            if (result == null)
                return BadRequest("User doesn't exists or the new password doesn't meet the requirements");

            return Ok(result);
        }
    }
}
