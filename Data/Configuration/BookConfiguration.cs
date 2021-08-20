using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mvcday1.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
             builder.HasKey(b => b.Id);
             builder.HasOne(b => b.Category)
                    .WithMany(b => b.Books)
                    .HasForeignKey(b => b.CategoryId);
        }
    }
}