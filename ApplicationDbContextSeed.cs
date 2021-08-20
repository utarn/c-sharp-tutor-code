using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mvcday1.Data;

namespace Mvcday1
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context) {
            // if (!await context.Categories.AnyAsync(c => c.Name == "Fiction")) {
            //     await context.Categories.AddAsync(new Category() { Name = "Fiction" });
            // }
                        
            if (!await context.Categories.AnyAsync()) {
                await context.Categories.AddAsync(new Category() { Name = "Fiction" });
                await context.Categories.AddAsync(new Category() { Name = "Non-Fiction" });
                await context.Categories.AddAsync(new Category() { Name = "Documentary" });
                await context.Categories.AddAsync(new Category() { Name = "Computer Science" });
                await context.SaveChangesAsync();
            }
            if (!await context.Books.AnyAsync(b => b.Title == "ASP.NET Core")) {
                await context.Books.AddAsync(new Book() {
                    Title = "ASP.NET Core",
                    Price = 100m,
                    Category = await context.Categories.FirstAsync(c => c.Name == "Computer Science")
                });
                await context.SaveChangesAsync();
            }
        }
    }
}