using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetBooksList
{
    public class GetBooksListQuery : IRequest<BooksListVM>
    {
        public class GetBooksListQueryHandler : IRequestHandler<GetBooksListQuery, BooksListVM>
        {
            private readonly IBookStoreDbContext db;
            private readonly IMapper mapper;

            public GetBooksListQueryHandler(IBookStoreDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<BooksListVM> Handle(GetBooksListQuery request, CancellationToken cancellationToken)
            {
                var books = await db.Books
                    .AsNoTracking()
                    .Include("Author")
                    .ProjectTo<BookLookupDTO>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new BooksListVM() { Books = books };
            }
        }
    }
}
