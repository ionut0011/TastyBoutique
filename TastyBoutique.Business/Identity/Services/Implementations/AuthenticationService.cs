using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest userAuthenticationModel)
        {
            var user = await _userRepository.GetByEmail(userAuthenticationModel.Email);

            return user == null || !_passwordHasher.Check(user.Password, userAuthenticationModel.Password) ? null : GenerateToken(user);
        }

        public async Task<UserModel> Register(UserRegisterModel userRegisterModel)
        {
            /*var user = await _userRepository.GetByEmail(userRegisterModel.Email);
            if (user != null)
            {
                return null;
            }*/
            var newStudent = new Student(userRegisterModel.studentModel.Name, userRegisterModel.studentModel.Age, userRegisterModel.Email);
            var newUser = new User(userRegisterModel.Username, userRegisterModel.Email, _passwordHasher.CreateHash(userRegisterModel.Password));
            newUser.IdStudentNavigation = newStudent;
            newUser.IdUserTypeNavigation = new UserType("a","b");

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
