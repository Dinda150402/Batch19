using Microsoft.AspNetCore.Identity;
using CRUDEFCore.Models;

namespace CRUDEFCore.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            const string adminUsername = "admin";
            var adminUser = await userManager.FindByNameAsync(adminUsername);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = adminUsername };
                var result = await userManager.CreateAsync(adminUser, "Admin123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("Seed: admin user dibuat -> username: admin, password: Admin123");
                }
            }
        }
    }
}
