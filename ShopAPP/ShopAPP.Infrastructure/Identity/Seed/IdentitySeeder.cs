using Microsoft.AspNetCore.Identity;
using ShopAPP.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPP.Infrastructure.Identity.Seed
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Seller", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            await CreateUserIfNotExists(userManager,
                email: "admin@shopapp.com",
                password: "Admin123!",
                role: "Admin",
                fullName: "Admin User",
                document: "00000000000");

            await CreateUserIfNotExists(userManager,
                email: "seller@shopapp.com",
                password: "Seller123!",
                role: "Seller",
                fullName: "Seller User",
                document: "11111111111");

            await CreateUserIfNotExists(userManager,
                email: "customer@shopapp.com",
                password: "Customer123!",
                role: "Customer",
                fullName: "Customer User",
                document: "22222222222");
        }

        private static async Task CreateUserIfNotExists(
            UserManager<ApplicationUser> userManager,
            string email,
            string password,
            string role,
            string fullName,
            string document)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    FullName = fullName,
                    Document = document,
                    IsActive = true,
                    UserType = role,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(newUser, password);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(newUser, role);
                else
                    throw new Exception($"Erro ao criar o usuário {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}
