using BookStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using BookStore.Application.Common.Exceptions;
using BookStore.Domain.Entities;

namespace BookStore.Application.Clients.Queries.GetClientDetail
{
    public class GetClientDetailQuery : IRequest<ClientDetailVM>
    {
        public int Id { get; set; }

        public class GetClientDetailQueryHandler : IRequestHandler<GetClientDetailQuery, ClientDetailVM>
        {
            private readonly IBookStoreDbContext db;

            public GetClientDetailQueryHandler(IBookStoreDbContext db)
            {
                this.db = db;
            }

            public async Task<ClientDetailVM> Handle(GetClientDetailQuery request, CancellationToken cancellationToken)
            {
                var client = await db.Clients
                    .AsNoTracking()
                    .Include(x => x.Orders)
                        .ThenInclude(x => x.Book)
                        .ThenInclude(x => x.Author)
                    .Select(c => new ClientDetailVM()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Orders = c.Orders.Select(order => new OrderVO
                        {
                            Ammount = order.Ammount,
                            AuthorFullName = order.Book.Author.FullName,
                            AuthorId = order.Book.AuthorId,
                            BookId = order.BookId,
                            BookName = order.Book.Name
                        })
                    })
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (client == null)
                    throw new NotFoundException(nameof(Client), request.Id);
                return client;
            }
        }
    }
}
