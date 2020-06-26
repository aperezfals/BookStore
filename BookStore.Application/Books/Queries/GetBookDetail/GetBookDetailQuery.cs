using AutoMapper;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetBookDetail
{
    public class GetBookDetailQuery : IRequest<BookDetailVM>
    {
        public int Id { get; set; }

        public class GetBookDetailQueryHandler : IRequestHandler<GetBookDetailQuery, BookDetailVM>
        {
            private readonly IBookStoreDbContext db;
            private readonly IMapper mapper;

            public GetBookDetailQueryHandler(IBookStoreDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<BookDetailVM> Handle(GetBookDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await db.Books.Include("Author")
                    .FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);

                if (entity == null)
                    throw new NotFoundException(nameof(Book), request.Id);

                return mapper.Map<BookDetailVM>(entity);
            }
        }
    }
}
