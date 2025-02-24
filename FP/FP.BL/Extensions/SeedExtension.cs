using FP.Core.Entities;
using FP.Core.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FP.BL.Extensions;

public static class SeedExtension
{
    public static void UseUserSeed(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            CreateRoles(roleManager).Wait();
            CreateUser(userManager).Wait();
        }
    }
    private static async Task CreateRoles(RoleManager<IdentityRole> _roleManager)
    {
        if (!await _roleManager.Roles.AnyAsync())
        {
            foreach (Roles item in Enum.GetValues(typeof(Roles)))
            {
                await _roleManager.CreateAsync(new IdentityRole(item.ToString()));
            }
        }
    }
    private static async Task CreateUser(UserManager<User> _userManager)
    {
        if (!await _userManager.Users.AnyAsync(u=> u.FullName == "admin"))
        {
            User user = new User
            {
                FullName = "admin",
                FIN = "XXXXXXX",
                Email = "admin@gmail.com",
                PassportImageUrl = "XXX",
            };
            var result = await _userManager.CreateAsync(user, "1234");
            await _userManager.AddToRoleAsync(user, nameof(Roles.Admin));
        }
    }
}
