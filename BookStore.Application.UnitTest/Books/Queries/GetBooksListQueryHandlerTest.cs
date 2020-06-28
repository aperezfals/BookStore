using BookStore.Application.Books.Queries.GetBooksList;
using BookStore.Application.UnitTest.Common;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using static BookStore.Application.Books.Queries.GetBooksList.GetBooksListQuery;

namespace BookStore.Application.UnitTest.Books.Queries
{
    [TestFixture]
    public class GetBooksListQueryHandlerTest : QueryTestBase
    {
        GetBooksListQueryHandler handler;
        
        [SetUp]
        public void Init()
        {
            handler = new GetBooksListQueryHandler(Context, Mapper);
        }

        [Test]
        public async Task GetBooksShouldHave2Books()
        {
            var result = await handler.Handle(new GetBooksListQuery(), CancellationToken.None);

            Assert.AreEqual(result.Books.Count, 2);
        }

        [Test]
        public async Task GetBooksSecondClientShouldBeNamedClient2()
        {
            var result = await handler.Handle(new GetBooksListQuery(), CancellationToken.None);

            Assert.AreEqual(result.Books.Count, 2);
        }
    }
}
