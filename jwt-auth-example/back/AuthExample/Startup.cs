using AuthExample.Data;
using AuthExample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourmalineCore.AspNetCore.JwtAuthentication.Core;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Options;
using TourmalineCore.AspNetCore.JwtAuthentication.Identity;
using TourmalineCore.AspNetCore.JwtAuthentication.Identity.Options;

namespace AuthExample
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("Database")
            );

            var authenticationOptions = (_configuration.GetSection(nameof(AuthenticationOptions)).Get<RefreshAuthenticationOptions>());
            services
                .AddJwtAuthenticationWithIdentity<AppDbContext, CustomUser>()
                .AddLoginWithRefresh(authenticationOptions)
                .AddRegistration()
                .AddLogout();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(
                builder => builder
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(host => true)
                    .AllowCredentials()
                    .AllowAnyMethod()
            );

            app.UseRouting();

            app.UseDefaultDbUser<AppDbContext, CustomUser>("Admin", "Admin");

            app
                .UseJwtAuthentication()
                .UseDefaultLoginMiddleware()
                .UseRefreshTokenMiddleware()
                .UseRefreshTokenLogoutMiddleware()
                .UseRegistration(requestModel => new CustomUser
                {
                    UserName = requestModel.Login,
                    NormalizedUserName = requestModel.Login,
                });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
