using BookStore.Domain.Entities;
using BookStore.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookStore.Application.UnitTest.Common
{
    public static class BookStoreContextFactory
    {
        public static BookStoreDbContext Create()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BookStoreDbContext(
                new MachineDateTime(),
                new AnonymousUserService(),
                options
                );

            context.Database.EnsureCreated();

            FillTestData(context);

            return context;
        }

        public static void Destroy(BookStoreDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private static void FillTestData(BookStoreDbContext context)
        {
            var client1 = new Client() { Name = "Client 1" };
            var client2 = new Client() { Name = "Client 2" };

            context.Clients.Add(client1);
            context.Clients.Add(client2);

            var author1 = new Author() { FullName = "Author 1" };
            var author2 = new Author() { FullName = "Author 2" };

            context.Authors.Add(author1);
            context.Authors.Add(author2);

            var book1 = new Book()
            {
                AuthorId = author1.Id,
                ISBN = "9788497592208",
                Name = "Book 1"
            };

            var book2 = new Book()
            {
                AuthorId = author2.Id,
                ISBN = "9788497592208",
                Name = "Book 2"
            };

            context.Books.Add(book1);
            context.Books.Add(book2);

            var order1 = new Order()
            {
                ClientId = client1.Id,
                BookId = book1.Id,
                Ammount = 20
            };

            var order2 = new Order()
            {
                ClientId = client1.Id,
                BookId = book2.Id,
                Ammount = 13
            };

            var order3 = new Order()
            {
                ClientId = client2.Id,
                BookId = book1.Id,
                Ammount = 50
            };

            var order4 = new Order()
            {
                ClientId = client2.Id,
                BookId = book2.Id,
                Ammount = 30
            };

            context.Orders.Add(order1);
            context.Orders.Add(order2);
            context.Orders.Add(order3);
            context.Orders.Add(order4);

            context.SaveChanges();
        }


    }
}
