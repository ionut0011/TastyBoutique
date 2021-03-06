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
using TastyBoutique.Business.Identity;
using TastyBoutique.Business.Identity.Models;
using TastyBoutique.Business.Identity.Services.Implementations;
using TastyBoutique.Business.Identity.Services.Interfaces;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using TastyBoutique.Persistance;
using TastyBoutique.Persistance.Identity;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;
using TripLooking.API.Extensions;
using TastyBoutique.Business.Identity.Services.Validators;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Repositories.Filters;
using Newtonsoft.Json;
using TastyBoutique.Business.Services.Implementations;
using TastyBoutique.Business.Services.Interfaces;
using TastyBoutique.Business.Services.Implementation;
using TastyBoutique.Business.Mapping;

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
            services.AddCors();
            services
                .AddMvc();
            
            services
                .AddSwaggerGen();
            services
                .AddScoped<IRecipeService, RecipeService>()
                .AddScoped<IRecipeRepository, RecipeRepository>()
                .AddScoped<IRecipeCommentService, RecipeCommentService>()
                .AddScoped<IPasswordHasher, PasswordHasher>()
                .AddScoped<IAuthenticationService, Business.Identity.Services.Implementations.AuthenticationService>()
                .AddScoped<ICollectionService, CollectionService>()
                .AddScoped<ICollectionRepository,CollectionRepository>()
                .AddScoped<INotificationService, NotificationService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IIngredientsRepository, IngredientsRepository>()
                .AddScoped<IIngredientService, IngredientService>()
                .AddScoped<IRecipeCommentService, RecipeCommentService>()
                .AddScoped<IFiltersRepository, FiltersRepository>()
                .AddScoped<IFilterService, FilterService>()
                .AddScoped<ISearchService, SearchService>()
                .AddDbContext<TastyBoutiqueContext>(config =>
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
                .AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            
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
                .UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
                .UseHttpsRedirection()
                .UseStaticFiles()
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
