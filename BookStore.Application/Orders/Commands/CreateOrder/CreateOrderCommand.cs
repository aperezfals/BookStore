using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int ClientId { get; set; }

        public int BookId { get; set; }

        public int Ammount { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
        {
            private readonly IBookStoreDbContext db;

            public CreateOrderCommandHandler(IBookStoreDbContext db)
            {
                this.db = db;
            }

            public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                await ValidateBook(request.BookId, cancellationToken);

                var entity = new Order()
                {
                    BookId = request.BookId,
                    ClientId = request.ClientId,
                    Ammount = request.Ammount
                };

                await db.Orders.AddAsync(entity, cancellationToken);

                await db.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }

            private async Task ValidateBook(int bookId, CancellationToken cancellationToken)
            {
                bool existBook = await db.Books.AnyAsync(book => book.Id == bookId, cancellationToken);
                if (!existBook)
                    throw new NotFoundException(nameof(Book), bookId);
            }

            private async Task ValidateClient(int clientId, CancellationToken cancellationToken)
            {
                bool existClient = await db.Clients.AnyAsync(client => client.Id == clientId, cancellationToken);
                if (!existClient)
                    throw new NotFoundException(nameof(Client), clientId);
            }
        }
    }
}
