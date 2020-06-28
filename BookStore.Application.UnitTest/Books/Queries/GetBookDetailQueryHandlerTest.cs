using BookStore.Application.Books.Queries.GetBookDetail;
using BookStore.Application.UnitTest.Common;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using static BookStore.Application.Books.Queries.GetBookDetail.GetBookDetailQuery;

namespace BookStore.Application.UnitTest.Books.Queries
{
    [TestFixture]
    public class GetBookDetailQueryHandlerTest : QueryTestBase
    {
        GetBookDetailQueryHandler handler;

        [SetUp]
        public void Init()
        {
            handler = new GetBookDetailQueryHandler(Context, Mapper);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task GetBookDetailShouldBeNotNull(int bookId)
        {
            var query = new GetBookDetailQuery() { Id = bookId };

            var book = await handler.Handle(query , CancellationToken.None);

            Assert.IsNotNull(book);
        }

        [TestCase(1, 70)]
        [TestCase(2, 43)]
        public async Task GetBookDetailShouldHaveRightOrdersAmmount(int bookId, int expectedAmmount)
        {
            var query = new GetBookDetailQuery() { Id = bookId };

            var book = await handler.Handle(query, CancellationToken.None);

            Assert.AreEqual(expectedAmmount, book.OrdersAmmount);
        }
    }
}
