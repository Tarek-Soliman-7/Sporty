using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticAssets;
using Sporty.Data;
using Sporty.Models;
namespace Sporty.Services
{
    public class SeedService
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                logger.LogInformation("Ensuring the database is created");
                await context.Database.EnsureCreatedAsync();

                logger.LogInformation("seeding roles");
                await AddRoleAsync(roleManager, "ClubAdmin");
                await AddRoleAsync(roleManager, "User");

                var adminEmail = "AlAhlyAdmin@gmail.com";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new User
                    {
                        FullName = "AlAhly Club",
                        UserName = adminEmail,
                        NormalizedUserName = adminEmail.ToUpper(),
                        Email = adminEmail,
                        ClubId = 1,
                        NormalizedEmail = adminEmail,
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    var result = await userManager.CreateAsync(adminUser, "Admin@123");
                    if (result.Succeeded)
                    {
                        logger.LogInformation("Assigning Admin Role to the admin user");
                        await userManager.AddToRoleAsync(adminUser, "ClubAdmin");
                    }
                    else
                    {
                        logger.LogError("Failed to create admin User :{Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));

                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("An error occurred while seeding database");
            }
        }


        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role '{roleName}':{string.Join(", '", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}