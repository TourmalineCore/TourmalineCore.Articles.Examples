using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaginationExample.Data;
using PaginationExample.Queries;

namespace PaginationExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "ApplicationDb"));
            
            services.AddTransient<ProductsQuery>();

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

            DataSeeder.InitDb(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
