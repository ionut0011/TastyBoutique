using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TastyBoutique.Business.Recipes;
using TastyBoutique.Business.Recipes.Services.Implementations;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TastyBoutique.Business;
using TastyBoutique.Business.Identity.Models;
using TastyBoutique.Business.Implementations.Services.Implementations;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Repositories.Filters;
using TripLooking.API.Extensions;

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
                .AddScoped<IIngredientService,IngredientService>()
                .AddScoped<IIngredientsRepo,IngredientsRepo>()
                .AddScoped<IFilterService,FilterService>()
                .AddScoped<IFiltersRepo,FiltersRepo>()
                .AddDbContext<TastyBoutique_v2Context>(config =>
                    config.UseSqlServer(Configuration.GetConnectionString("TastyConnection")));
            services
                .AddAutoMapper(c =>
                {
                    c.AddProfile<Mapping>();
                }, typeof(RecipeService), typeof(IngredientService), typeof(FilterService))

                .AddHttpContextAccessor()
                .AddSwagger();
                
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
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trips API"));

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
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
