using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Common.Interfaces
{
    public interface IBookStoreDbContext
    {
        DbSet<Book> Books { get; set; }

        DbSet<Client> Clients { get; set; }

        DbSet<Author> Authors { get; set; }

        DbSet<Order> Orders { get; set; }
    }
}
