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
using TastyBoutique.Business.Collections.Services.Implementation;
using TastyBoutique.Business.Collections.Services.Interfaces;
using TastyBoutique.Business.Identity.Models;
using TastyBoutique.Business.Identity.Services.Implementations;
using TastyBoutique.Business.Identity.Services.Interfaces;
using TastyBoutique.Business.Implementations.Services.Implementations;
using TastyBoutique.Business.Recipes;
using TastyBoutique.Business.Recipes.Services.Implementations;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using TastyBoutique.Persistance;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;
using TripLooking.API.Extensions;
using AuthenticationService = Microsoft.AspNetCore.Authentication.AuthenticationService;
using IAuthenticationService = Microsoft.AspNetCore.Authentication.IAuthenticationService;

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
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<ICollectionService, ICollectionService>()
                .AddScoped<ICollectionRepo,CollectionRepo>()
                .AddDbContext<TastyBoutique_v2Context>(config =>
                    config.UseSqlServer(Configuration.GetConnectionString("TastyConnection")));
            services
                .AddAutoMapper(c =>
                {
                    c.AddProfile<Mapping>();
                }, typeof(RecipeService), typeof(IngredientService), typeof(FilterService), typeof(CollectionService), typeof(RecipeCommentService), typeof(AuthenticationService))

                .AddHttpContextAccessor()
                .AddSwagger();
            services.AddControllers().AddXmlDataContractSerializerFormatters();



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
