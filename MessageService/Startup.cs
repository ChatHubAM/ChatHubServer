using AutoMapper;
using DbAccessLayer.Context;
using DbAccessLayer.Repository;
using IdentityServer4.AccessTokenValidation;
using MessageService.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageService
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

            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("ChatHubDatabase"));
            });

            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //       .AddIdentityServerAuthentication(options =>
            //       {
            //           options.Authority = "http://localhost:5000";
            //           options.RequireHttpsMetadata = false;

            //           options.ApiName = "ActiveUserService";
            //       }
            //   );


            services.AddScoped<IMessageRepository, MessageRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MessageDTOMessageEntity());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //app.UseAuthentication();
          //  app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
