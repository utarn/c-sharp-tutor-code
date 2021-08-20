using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mvcday1.Data;

namespace Mvcday1
{
    public static class ApplicationDbContextSeed
    {

        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminRole = new IdentityRole("Administrator");
            if (await roleManager.Roles.AllAsync(r => r.Name != adminRole.Name))
            {
                await roleManager.CreateAsync(adminRole);
            }
            var moderatorRole = new IdentityRole("Moderator");

            if (await roleManager.Roles.AllAsync(r => r.Name != moderatorRole.Name))
            {
                await roleManager.CreateAsync(moderatorRole);
            }

            var userRole = new IdentityRole("User");

            if (await roleManager.Roles.AllAsync(r => r.Name != userRole.Name))
            {
                await roleManager.CreateAsync(userRole);
            }


            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                var appUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@localhost",
                    FirstName = "Default Admin",
                    LastName = "",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(appUser, "Admin123!!");
                await userManager.AddToRoleAsync(appUser, "Administrator");
            }
        }
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // if (!await context.Categories.AnyAsync(c => c.Name == "Fiction")) {
            //     await context.Categories.AddAsync(new Category() { Name = "Fiction" });
            // }

            if (!await context.Categories.AnyAsync())
            {
                await context.Categories.AddAsync(new Category() { Name = "Fiction" });
                await context.Categories.AddAsync(new Category() { Name = "Non-Fiction" });
                await context.Categories.AddAsync(new Category() { Name = "Documentary" });
                await context.Categories.AddAsync(new Category() { Name = "Computer Science" });
                await context.SaveChangesAsync();
            }
            if (!await context.Books.AnyAsync(b => b.Title == "ASP.NET Core"))
            {
                await context.Books.AddAsync(new Book()
                {
                    Title = "ASP.NET Core",
                    Price = 100m,
                    Category = await context.Categories.FirstAsync(c => c.Name == "Computer Science")
                });
                await context.SaveChangesAsync();
            }
        }
    }
}