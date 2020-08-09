using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using TastyBoutique.API;
using Moq;
using Xunit;
using TastyBoutique.Persistance.Models;
using Microsoft.Extensions.DependencyInjection;
using TastyBoutique.Business.Identity.Models;
using FluentAssertions;
using System.Text.RegularExpressions;

namespace TastyBoutique.IntegrationTesting
{
    public class IntegrationTests : IAsyncLifetime
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;

        protected HttpClient HttpClient { get; private set; }

        protected string AuthenticationToken { get; private set; }

        protected Guid AuthenticatedUserId { get; private set; }

        //protected Mock<IDomainLogger> MockLogger { get; private set; }

        protected IntegrationTests()
        {
            //MockLogger = new Mock<IDomainLogger>();
            webApplicationFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder => {});
            HttpClient = webApplicationFactory.CreateClient();
        }

        async Task IAsyncLifetime.InitializeAsync()
        {
            await Task.Run(async () =>
            {
                await ExecuteDatabaseAction(async (TastyBoutiqueContext) => await ClearDatabase(TastyBoutiqueContext));
                await SetAuthenticationToken();
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationToken);
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

        private async Task SetAuthenticationToken()
        {
            var userRegisterModel = new UserRegisterModel
            {
                Name = "Cristi Martin",
                Age = 21,
                Username="cristi.martin",
                Email = "cristi.martin@gmail.com",
                Password = "String12"
            };
            var userRegisterResponse = await HttpClient.PostAsJsonAsync($"api/v1/auth/register", userRegisterModel);
            userRegisterResponse.IsSuccessStatusCode.Should().BeTrue();
            AuthenticatedUserId = new Guid(userRegisterResponse.Headers.Location.OriginalString);
            var user = new User("Cristi Martin", "cristi.martin@gmail.com", "123456", "user");
            var authenticateModel = new AuthenticationRequest
            {
                Email = user.Email,
                Password = userRegisterModel.Password
            };
            var userAuthenticateResponse = await HttpClient.PostAsJsonAsync($"api/v1/auth/login", authenticateModel);
            userAuthenticateResponse.IsSuccessStatusCode.Should().BeTrue();
            var authenticationResponseContent = await userAuthenticateResponse.Content.ReadAsAsync<AuthenticationResponse>();

            AuthenticationToken = authenticationResponseContent.Token;
        }

        protected Guid Extract_Guid(string str)
        {
            string guidPattern = @"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}";

            return Guid.Parse(Regex.Match(str, guidPattern).Value);
        }

        protected async Task<Recipes> AddRecipe()
        {
            var image = new byte[] { 1, 0, 0, 1, 8 };
            var recipe = new Recipes("Cartofi copti", "meal", true, "Foarte buni", image);
            recipe.RecipesIngredients.Add(new RecipesIngredients(recipe, new Ingredients("cartofi")));
            recipe.RecipesIngredients.Add(new RecipesIngredients(recipe, new Ingredients("ulei")));
            recipe.RecipesIngredients.Add(new RecipesIngredients(recipe, new Ingredients("morcov")));
            recipe.RecipesIngredients.Add(new RecipesIngredients(recipe, new Ingredients("ceapa")));
            recipe.RecipesFilters.Add(new RecipesFilters(recipe, new Filters("vegan")));
            recipe.IdUser = AuthenticatedUserId;

            await ExecuteDatabaseAction(async (tastyBoutiqueContext) =>
            {
                await tastyBoutiqueContext.Recipes.AddAsync(recipe);
                await tastyBoutiqueContext.SaveChangesAsync();
            });

            return recipe;
        }
    }
}