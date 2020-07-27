using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Identity.Models;

namespace TastyBoutique.Business.Identity.Services.Interfaces
{
    public interface IAuthenticationService
    {

        Task<AuthenticationResponse> Authenticate(AuthenticationRequest userAuthenticationModel);

        Task<UserModel> Register(UserRegisterModel userRegisterModel);
    }
}
