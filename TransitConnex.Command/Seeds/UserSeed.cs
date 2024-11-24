using Microsoft.AspNetCore.Identity;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public static class UserSeed
{
    public static async Task Seed(UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        await SeedRoles(roleManager);
        await SeedAdmin(userManager);
        await SeedNormalUser(userManager);
    }

    private static void HandleCreateResult(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }
            throw new ApplicationException("Failed to seed user");
        }
    }

    private const string RoleAdmin = "Admin";
    private static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
    {
        var result = await roleManager.CreateAsync(new IdentityRole<Guid>(RoleAdmin));
        HandleCreateResult(result);
    }

    private static async Task SeedAdmin(UserManager<User> userManager)
    {
        var adminId = Guid.Parse("d6f46418-2222-4444-bbbb-162fb5e3a999");
        if (await userManager.FindByIdAsync(adminId.ToString()) == null)
        {
            const string adminEmail = "admin@example.com";
            const string adminPassword = "Admin123";
            var adminlUser = new User()
            {
                Id = adminId,
                UserName = adminEmail,
                Email = adminEmail,
                Created = DateTime.Parse("2024-11-14"),
                Updated = DateTime.Parse("2024-11-14"),
                Deleted = false,
                IsAdmin = true,
            };
            var result = await userManager.CreateAsync(adminlUser, adminPassword);
            HandleCreateResult(result);

            // Add the user role
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin is not null)
            {
                var roleAddResult = await userManager.AddToRoleAsync(admin, RoleAdmin);
                HandleCreateResult(roleAddResult);
            }
        }
    }
    private static async Task SeedNormalUser(UserManager<User> userManager)
    {
        var userId = Guid.Parse("d6f46418-2c21-43f8-b167-162fb5e3a999");
        if (await userManager.FindByIdAsync(userId.ToString()) == null)
        {
            const string userEmail = "user@example.com";
            const string userPassword = "Admin123";
            var user = new User()
            {
                Id = userId,
                UserName = userEmail,
                Email = userEmail,
                Created = DateTime.Parse("2024-11-14"),
                Updated = DateTime.Parse("2024-11-14"),
                Deleted = false,
                IsAdmin = true,
            };
            var result = await userManager.CreateAsync(user, userPassword);
            HandleCreateResult(result);
        }
    }
}
