using System;
using System.IO;
using IdentityServer4;
using DbAccessLayer.Repository;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityServer.Validator;
using DbAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Added IdentityServer4 to a configurations
            services.AddIdentityServer()
               .AddDeveloperSigningCredential()
               .AddInMemoryApiResources(Config.GetApis())
               .AddInMemoryIdentityResources(Config.GetIdentityResources())
               .AddInMemoryClients(Config.GetClients())
               .AddProfileService<ProfileService>();

            services.AddAuthentication()
                .AddIdentityServerAuthentication(o =>
                {
                    o.RequireHttpsMetadata = true;
                });

            //Accesing Db via connection string
            services.AddDbContext<AppDbContext>(c => c.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ChatHub;Trusted_Connection=True;"));
           
            //DI for User Repository pattern
            services.AddScoped<IUserRepository, UserRepository>();
            //DI for password validation
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            //DI for Profile service class
            services.AddTransient<IProfileService, ProfileService>();
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseIdentityServer();
            


            //if (Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler();
            //}

            //uncomment if you want to support static files
            //app.UseStaticFiles();


            // uncomment, if you wan to add an MVC-based UI
            //app.UseMvcWithDefaultRoute();
        }
    }
}