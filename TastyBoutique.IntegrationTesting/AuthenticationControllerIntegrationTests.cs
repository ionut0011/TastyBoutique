using FluentAssertions;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Identity.Models;
using TastyBoutique.Persistance.Models;
using Xunit;

namespace TastyBoutique.IntegrationTesting
{
    public class AuthenticationControllerIntegrationTests :IAsyncLifetime
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;

        protected HttpClient HttpClient { get; private set; }

        public AuthenticationControllerIntegrationTests()
        {
            webApplicationFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder => { });
            HttpClient = webApplicationFactory.CreateClient();
        }

        async Task IAsyncLifetime.InitializeAsync()
        {
            await Task.Run(async () =>
            {
                await ExecuteDatabaseAction(async (TastyBoutiqueContext) => await ClearDatabase(TastyBoutiqueContext));
            });
        }

        Task IAsyncLifetime.DisposeAsync()
        {
            return Task.CompletedTask;
        }

        protected async Task ExecuteDatabaseAction(Func<TastyBoutiqueContext, Task> databaseAction)
        {
            using (var scope = webApplicationFactory.Services.CreateScope())
            {
                var tripsContext = scope.ServiceProvider.GetRequiredService<TastyBoutiqueContext>();

                await databaseAction(tripsContext);
            }
        }

        private async Task ClearDatabase(TastyBoutiqueContext tastyBoutiqueContext)
        {
            tastyBoutiqueContext.Recipes.RemoveRange(tastyBoutiqueContext.Recipes);
            tastyBoutiqueContext.User.RemoveRange(tastyBoutiqueContext.User);
            await tastyBoutiqueContext.SaveChangesAsync();

            tastyBoutiqueContext.Filters.RemoveRange(tastyBoutiqueContext.Filters);
            tastyBoutiqueContext.Ingredients.RemoveRange(tastyBoutiqueContext.Ingredients);
            await tastyBoutiqueContext.SaveChangesAsync();
        }

        [Fact]
        public async Task Register_Invalid_Credentials()
        {
            var userRegisterModel = new UserRegisterModel
            {
                Age = 21,
                Email = "test",
                Name = "test",
                Password = "test",
                Username = "test"
            };
            var userRegisterResponse = await HttpClient.PostAsJsonAsync($"api/v1/auth/register", userRegisterModel);
            userRegisterResponse.IsSuccessStatusCode.Should().BeFalse();
        }

        [Fact]
        public async Task Login_Invalid_Credentials()
        {
            var loginModel = new AuthenticationRequest
            {
                Email = "test",
                Password = "test"
            };
            var response = await HttpClient.PostAsJsonAsync($"api/v1/auth/login", loginModel);

            response.IsSuccessStatusCode.Should().BeFalse();
        }

        [Fact]
        public async Task Login_Valid_Credentials()
        {
            var userRegisterModel = new UserRegisterModel
            {
                Age = 21,
                Email = "test@gmail.com",
                Name = "test",
                Password = "TameImpala13",
                Username = "TameImpala"
            };

            var response = await HttpClient.PostAsJsonAsync($"api/v1/auth/register", userRegisterModel);
            response.IsSuccessStatusCode.Should().BeTrue();

            var loginModel = new AuthenticationRequest
            {
                Email = userRegisterModel.Email,
                Password = userRegisterModel.Password
            };

            response = await HttpClient.PostAsJsonAsync($"api/v1/auth/login", loginModel);
            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task Change_Password()
        {
            var userRegisterModel = new UserRegisterModel
            {
                Age = 21,
                Email = "test@gmail.com",
                Name = "test",
                Password = "TameImpala13",
                Username = "TameImpala"
            };

            var response = await HttpClient.PostAsJsonAsync($"api/v1/auth/register", userRegisterModel);
            response.IsSuccessStatusCode.Should().BeTrue();

            var userNewPasswordModel = new UserNewPasswordModel
            {
                Email = userRegisterModel.Email,
                NewPassword = "TameImpala12",
            };

            response = await HttpClient.PostAsJsonAsync($"api/v1/auth/recover", userNewPasswordModel);
            response.IsSuccessStatusCode.Should().BeTrue();
            var loginModel = new AuthenticationRequest
            {
                Email = userRegisterModel.Email,
                Password = userNewPasswordModel.NewPassword
            };

            response = await HttpClient.PostAsJsonAsync($"api/v1/auth/login", loginModel);
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}
