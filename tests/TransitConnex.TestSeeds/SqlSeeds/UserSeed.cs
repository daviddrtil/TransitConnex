using Microsoft.AspNetCore.Identity;
using TransitConnex.Domain.DTOs.User;
using TransitConnex.Domain.Models;

namespace TransitConnex.TestSeeds.SqlSeeds;

public static class UserSeed
{
    #region SeedData
    public const string AdminRole = "Admin";
    public const string AdminEmail = "admin@example.com";
    public const string AdminPassword = "Admin123";
    public static readonly User AdminUser = new()
    {
        Id = Guid.Parse("d6f46418-2222-4444-bbbb-162fb5e3a999"),
        Email = AdminEmail,
        UserName = AdminEmail,
        Created = DateTime.Parse("2024-11-14"),
        Updated = DateTime.Parse("2024-11-14"),
        Deleted = false,
        IsAdmin = true,
    };

    public const string BasicUserEmail = "user@example.com";
    public const string BasicUserPassword = "Basic123";
    public static readonly User BasicUser = new()
    {
        Id = Guid.Parse("39123a3c-3ce3-4bcc-8887-eb7d8e975ea8"),
        Email = BasicUserEmail,
        UserName = BasicUserEmail,
        IsAdmin = false,
    };

    public static readonly LoginDto AdminLogin = new()
    {
        Email = AdminUser.Email,
        Password = AdminPassword
    };

    public static readonly LoginDto BasicLogin = new()
    {
        Email = BasicUser.Email,
        Password = BasicUserPassword
    };
    #endregion SeedData

    public static async Task Seed(
        UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        await SeedRoles(roleManager);
        await SeedAdmin(userManager);
        await SeedBasicUser(userManager);
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

    private static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
    {
        var result = await roleManager.CreateAsync(new IdentityRole<Guid>(AdminRole));
        HandleCreateResult(result);
    }

    private static async Task SeedAdmin(UserManager<User> userManager)
    {
        if (await userManager.FindByEmailAsync(AdminEmail) is null)
        {
            var result = await userManager.CreateAsync(AdminUser, AdminPassword);
            HandleCreateResult(result);

            // Add admin role
            var admin = await userManager.FindByEmailAsync(AdminEmail);
            if (admin is not null)
            {
                var roleAddResult = await userManager.AddToRoleAsync(admin, AdminRole);
                HandleCreateResult(roleAddResult);
            }
        }
    }

    private static async Task SeedBasicUser(UserManager<User> userManager)
    {
        if (await userManager.FindByEmailAsync(BasicUserEmail) is null)
        {
            var result = await userManager.CreateAsync(BasicUser, BasicUserPassword);
            HandleCreateResult(result);
        }
    }
}
