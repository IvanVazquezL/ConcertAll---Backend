using ConcertAll.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertAll.Persistence
{
    public static class UserDataSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            //  User repository
            var userManager = service.GetRequiredService<UserManager<ConcertAllUserIdentity>>();
            //  Role repository
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            //  Creating roles
            var adminRole = new IdentityRole(Constants.RoleAdmin);
            var customerRole = new IdentityRole(Constants.RoleCustomer);

            if (!await roleManager.RoleExistsAsync(Constants.RoleAdmin))
                await roleManager.CreateAsync(adminRole);

            if (!await roleManager.RoleExistsAsync(Constants.RoleCustomer))
                await roleManager.CreateAsync(customerRole);

            //  Admin user
            var adminUser = new ConcertAllUserIdentity()
            {
                FirstName = "System",
                LastName = "Administrator",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                PhoneNumber = "1234567890",
                Age = 35,
                DocumentType = DocumentTypeEnum.Dni,
                DocumentNumber = "1234567890",
                EmailConfirmed = true
            };

            if (await userManager.FindByEmailAsync("admin@gmail.com") is null)
            {
                var result = await userManager.CreateAsync(adminUser, "Admin1234*");
                if (result.Succeeded)
                {
                    //  Retrieve the user record
                    adminUser = await userManager.FindByEmailAsync(adminUser.Email);
                    //  Add admin role to admin user
                    if (adminUser is not null)
                        await userManager.AddToRoleAsync(adminUser, Constants.RoleAdmin);
                }
            }
        }
    }
}
