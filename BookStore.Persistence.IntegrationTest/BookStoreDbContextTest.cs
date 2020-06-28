using BookStore.Application.Common.Interfaces;
using BookStore.Common;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BookStore.Persistence.IntegrationTest
{
    [TestFixture]
    public class BookStoreDbContextTest : IDisposable
    {
        private readonly string userId;
        private readonly DateTime dateTime;
        private readonly Mock<IDateTime> dateTimeMock;
        private readonly Mock<ICurrentUserService> currentUserServiceMock;
        private readonly BookStoreDbContext db;

        public BookStoreDbContextTest()
        {
            dateTime = new DateTime(3001, 1, 1);
            dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.Now).Returns(dateTime);

            userId = "00000000-0000-0000-0000-000000000000";
            currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock.Setup(m => m.UserId).Returns(userId);

            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            db = new BookStoreDbContext(dateTimeMock.Object, currentUserServiceMock.Object, options);

            db.Books.Add(new Book
            {
                Name = "book 1",
                ISBN = "9789089986542"
            });

            db.SaveChanges();
        }

        [Test]
        public async Task SaveChangesAsyncGivenNewBookShouldSetAuditableProperties()
        {
            var book = new Book
            {
                Name = "book 1",
                ISBN = "9789089986542"
            };

            db.Books.Add(book);

            await db.SaveChangesAsync();

            Assert.AreEqual(dateTime, book.Created);
            Assert.AreEqual(userId, book.CreatedBy);
        }

        [Test]
        public async Task SaveChangesAsyncGivenExistingBookShouldSetAuditableProperties()
        {
            var book = await db.Books.FindAsync(1);

            book.Name = "updated book";

            await db.SaveChangesAsync();

            Assert.IsNotNull(book.LastUpdatedBy);
            Assert.AreEqual(userId, book.LastUpdatedBy);
            Assert.AreEqual(dateTime, book.LastUpdated);
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}