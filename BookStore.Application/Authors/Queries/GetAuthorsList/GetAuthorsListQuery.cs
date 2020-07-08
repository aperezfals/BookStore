using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Authors.Queries.GetAuthorsList
{
    public class GetAuthorsListQuery : IRequest<AuthorsListVM>
    {
        public class GetAuthorsListQueryHandler : IRequestHandler<GetAuthorsListQuery, AuthorsListVM>
        {
            private readonly IBookStoreDbContext db;
            private readonly IMapper mapper;

            public GetAuthorsListQueryHandler(IBookStoreDbContext db,
                IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<AuthorsListVM> Handle(GetAuthorsListQuery request, CancellationToken cancellationToken)
            {
                var authors = await db.Authors
                    .AsNoTracking()
                    .ProjectTo<AuthorDTO>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new AuthorsListVM() { Authors = authors };
            }
        }
    }
}
