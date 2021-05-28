using System.Threading.Tasks;
using AuthExample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AuthExample.Data
{
    public class DataSeeder
    {
        public static async Task Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

            var adminRole = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
            };

            await context.Roles.AddAsync(adminRole);
            await context.SaveChangesAsync();

            var adminUser = new CustomUser
            {
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
            };
            adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "12345");

            await context.Users.AddAsync(adminUser);
            await context.SaveChangesAsync();

            await context.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id,
            });
            await context.SaveChangesAsync();
        }
    }
}
