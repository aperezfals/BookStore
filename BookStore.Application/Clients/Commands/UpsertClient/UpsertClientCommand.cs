using BookStore.Application.Common.Exceptions;
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

namespace BookStore.Application.Clients.Commands.UpsertClient
{
    public class UpsertClientCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public class UpsertClientCommandHandler : IRequestHandler<UpsertClientCommand, int>
        {
            private readonly IBookStoreDbContext db;

            public UpsertClientCommandHandler(IBookStoreDbContext db)
            {
                this.db = db;
            }

            public async Task<int> Handle(UpsertClientCommand request, CancellationToken cancellationToken)
            {
                Client entity;

                if (request.Id.HasValue)
                {
                    entity = await db.Clients.FirstOrDefaultAsync(x => x.Id == request.Id.Value, cancellationToken);
                    if(entity == null)
                        throw new NotFoundException(nameof(Client), request.Id.Value);
                }
                else
                {
                    entity = new Client();
                    db.Clients.Add(entity);
                }

                entity.Name = request.Name;

                await db.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
