using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.ISBN)
                .HasMaxLength(13)
                .IsRequired()
                .HasConversion(typeof(string));

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(x => x.Author)
               .WithMany(x => x.Books)
               .HasForeignKey(x => x.AuthorId)
               .IsRequired();
        }
    }
}
