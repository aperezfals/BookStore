using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Clients.Commands.DeleteClient
{
    public class DeleteClientCommand : IRequest
    {
        public int Id { get; set; }
        public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
        {
            private readonly IBookStoreDbContext db;

            public DeleteClientCommandHandler(IBookStoreDbContext db)
            {
                this.db = db;
            }
            public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Clients.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if(entity == null)
                    throw new NotFoundException(nameof(Client), request.Id);

                db.Clients.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
