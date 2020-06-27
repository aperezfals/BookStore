using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using MediatR;
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
        }
    }
}
