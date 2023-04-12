using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syntax.Auth.Data
{
    public static class RoleInitializer
    {
        public static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Administrador", "User", "PremiumUser" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
