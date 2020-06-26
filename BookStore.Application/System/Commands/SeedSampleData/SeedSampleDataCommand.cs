using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace BookStore.Application.System.Commands.SeedSampleData
{
    public class SeedSampleDataCommand : IRequest
    {
        public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand>
        {
            private readonly IBookStoreDbContext db;

            public SeedSampleDataCommandHandler(IBookStoreDbContext db)
            {
                this.db = db;
            }

            public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
            {
                if (!db.Authors.Any())
                {
                    await db.Authors.AddAsync(new Author { FullName = "Gabriel García Márquez" }, cancellationToken);
                    await db.Authors.AddAsync(new Author { FullName = "Ken Follet" }, cancellationToken);
                    await db.Authors.AddAsync(new Author { FullName = "Jorge Luis Borges" }, cancellationToken);
                    await db.Authors.AddAsync(new Author { FullName = "Carilda Oliver Labra" }, cancellationToken);

                    await db.SaveChangesAsync(cancellationToken);
                }

                return Unit.Value;
            }
        }
    }
}
