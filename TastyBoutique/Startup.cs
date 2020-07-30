using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using Newtonsoft.Json;
using TastyBoutique.Business.Collections.Services.Implementation;
using TastyBoutique.Business.Collections.Services.Interfaces;
using TastyBoutique.Business.Identity;
using TastyBoutique.Business.Identity.Models;
using TastyBoutique.Business.Identity.Services.Implementations;
using TastyBoutique.Business.Identity.Services.Interfaces;
using TastyBoutique.Business.Implementations.Services.Implementations;
using TastyBoutique.Business.Recipes;
using TastyBoutique.Business.Recipes.Services.Implementations;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using TastyBoutique.Persistance;
using TastyBoutique.Persistance.Identity;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;
using TripLooking.API.Extensions;
using AuthenticationService = Microsoft.AspNetCore.Authentication.AuthenticationService;
using IAuthenticationService = Microsoft.AspNetCore.Authentication.IAuthenticationService;
using TastyBoutique.Business.Identity.Services.Validators;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Repositories.Filters;

namespace TastyBoutique
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc();
            services
                .AddSwaggerGen();
            services
                .AddScoped<IRecipeService, RecipeService>()
                .AddScoped<IRecipeRepo, RecipeRepo>()
                .AddScoped<IRecipeCommentService, RecipeCommentService>()
                .AddScoped<IPasswordHasher, PasswordHasher>()
                .AddScoped<Business.Identity.Services.Interfaces.IAuthenticationService,Business.Identity.Services.Implementations.AuthenticationService>()
                .AddScoped<ICollectionService, CollectionService>()
                .AddScoped<ICollectionRepo,CollectionRepo>()
                .AddScoped<INotificationService, NotificationService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IIngredientsRepo, IngredientsRepo>()
                .AddScoped<IIngredientService, IngredientService>()
                .AddScoped<IRecipeCommentService, RecipeCommentService>()
                .AddScoped<IFiltersRepo, FiltersRepo>()
                .AddScoped<IFilterService, FilterService>()
                .AddDbContext<TastyBoutique_v2Context>(config =>
                    config.UseSqlServer(Configuration.GetConnectionString("TastyConnection")));
            services
                .AddAutoMapper(
                    c =>
                    {
                        c.AddProfile<Mapping>();
                        c.AddProfile<IdentityMappingProfile>();
                    }, typeof(RecipeService), typeof(IngredientService), typeof(FilterService),
                    typeof(CollectionService),
                    typeof(RecipeCommentService))

                .AddHttpContextAccessor()
                .AddSwagger()
                .AddControllers();

            services.AddTransient<IValidator<UserRegisterModel>, UserRegisterModelValidator>();
            AddAuthentication(services);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TastyBoutique API"));

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private void AddAuthentication(IServiceCollection services)
        {
            var jwtOptions = Configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"));

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Key)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience
                    };
                });
        }
    }
}
