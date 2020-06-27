using AutoMapper;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Commands.UpsertBook
{
    public class UpsertBookCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int AuthorId { get; set; }

        public string ISBN { get; set; }

        public class UpsertBookCommandHandler : IRequestHandler<UpsertBookCommand, int>
        {
            private readonly IBookStoreDbContext db;

            public UpsertBookCommandHandler(IBookStoreDbContext db)
            {
                this.db = db;
            }

            public async Task<int> Handle(UpsertBookCommand request, CancellationToken cancellationToken)
            {
                Book entity;

                if (request.Id.HasValue)
                {
                    entity = await db.Books.FirstOrDefaultAsync(x => x.Id == request.Id.Value, cancellationToken);
                    if(entity == null)
                        throw new NotFoundException(nameof(Book), request.Id.Value);
                }
                else
                {
                    entity = new Book();
                    db.Books.Add(entity);
                }

                await ValidateAuthor(request, cancellationToken);

                entity.AuthorId = request.AuthorId;
                entity.ISBN = request.ISBN;
                entity.Name = request.Name;

                await db.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }

            private async Task ValidateAuthor(UpsertBookCommand request, CancellationToken cancellationToken)
            {
                bool existAuthor = await db.Authors.AnyAsync(author => author.Id == request.AuthorId, cancellationToken);
                if (!existAuthor)
                    throw new NotFoundException(nameof(Author), request.AuthorId);
            }
        }
    }
}
