﻿using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteBookCommand>
        {
            private readonly IBookStoreDbContext db;

            public DeleteProductCommandHandler(IBookStoreDbContext db)
            {
                this.db = db;
            }

            public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Books.FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);
                if (entity == null)
                    throw new NotFoundException(nameof(Book), request.Id);

                bool hasOrders = await db.Orders.AnyAsync(order => order.ClientId == request.Id, cancellationToken);
                if (hasOrders)
                    throw new DeleteFailureException(nameof(Book), request.Id, "There are exisitng orders associated with this book.");

                db.Books.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
