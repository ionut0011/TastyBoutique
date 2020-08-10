using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TastyBoutique.Business.Identity.Models;
using TastyBoutique.Business.Identity.Services.Interfaces;
using TastyBoutique.Persistance.Identity;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Business.Identity.Services.Implementations
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly JwtOptions _config;

        public AuthenticationService(
           IUserRepository userRepository,
           IPasswordHasher passwordHasher,
           IMapper mapper,
           IOptions<JwtOptions> config)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _config = config.Value;
        }

        public async Task<UserModel> ForgotPassword(UserNewPasswordModel userNewPasswordModel)
        {
            var user = await _userRepository.GetByEmail(userNewPasswordModel.Email);
            var regexPassword = Regex.Match(userNewPasswordModel.NewPassword, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$");
            if (user == null || !regexPassword.Success)
                return null;

            user.Password = _passwordHasher.CreateHash(userNewPasswordModel.NewPassword);
            _userRepository.Update(user);
            await _userRepository.SaveChanges();
            
            return _mapper.Map<UserModel>(user);
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest userAuthenticationModel)
        {
            var user = await _userRepository.GetByEmail(userAuthenticationModel.Email);

            return user == null || !_passwordHasher.Check(user.Password, userAuthenticationModel.Password) ? null : GenerateToken(user);
        }

        public async Task<UserModel> Register(UserRegisterModel userRegisterModel)
        {
            var user = await _userRepository.GetByEmail(userRegisterModel.Email);
            var regexPassword = Regex.Match(userRegisterModel.Password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$");
            if (user != null || !regexPassword.Success)
            {
                return null;
            }
            var newUser = new User(userRegisterModel.Username, userRegisterModel.Email, _passwordHasher.CreateHash(userRegisterModel.Password), "user")
            {
                IdStudentNavigation = new Student(userRegisterModel.Name, userRegisterModel.Age, userRegisterModel.Email),
            };

            await _userRepository.Add(newUser);
            await _userRepository.SaveChanges();

            return _mapper.Map<UserModel>(newUser);
        }

        private AuthenticationResponse GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var hours = int.Parse(_config.TokenExpirationInHours);

            var token = new JwtSecurityToken(_config.Issuer,
                _config.Audience,
                new List<Claim>()
                {
                    new Claim("IdUser", user.Id.ToString())
                },
                expires: DateTime.Now.AddHours(hours),
                signingCredentials: credentials);

            return new AuthenticationResponse(user.Username, new JwtSecurityTokenHandler().WriteToken(token), user.Email);
        }
    }
}
