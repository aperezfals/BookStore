using AutoMapper;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
                var query = from book in db.Books
                            join author in db.Authors on book.AuthorId equals author.Id
                            join order in db.Orders on book.Id equals order.BookId into orders
                            from order in orders.DefaultIfEmpty()
                            where book.Id == request.Id
                            group new { book, author, order } by new
                            {
                                book.Id,
                                book.Name,
                                book.ISBN,
                                AuthorId = author.Id,
                                AuthorName = author.FullName
                            } into bookGroup
                            select new BookDetailVM
                            {
                                Id = bookGroup.Key.Id,
                                ISBN = bookGroup.Key.ISBN,
                                Author = new AuthorDTO()
                                {
                                    Id = bookGroup.Key.AuthorId,
                                    FullName = bookGroup.Key.AuthorName
                                },
                                Name = bookGroup.Key.Name,
                                AuthorId = bookGroup.Key.AuthorId,
                                OrdersAmmount = bookGroup.Sum(x => x.order.Ammount)
                            };

                var entity = await query.FirstOrDefaultAsync(cancellationToken);

                if (entity == null)
                    throw new NotFoundException(nameof(Book), request.Id);

                return mapper.Map<BookDetailVM>(entity);
            }
        }
    }
}
