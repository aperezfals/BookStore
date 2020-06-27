using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Clients.Queries.GetClientsListQuery
{
    public class GetClientsListQuery : IRequest<ClientsListVM>
    {
        public class GetClientsListQueryHandler : IRequestHandler<GetClientsListQuery, ClientsListVM>
        {
            private readonly IBookStoreDbContext db;
            private readonly IMapper mapper;

            public GetClientsListQueryHandler(IBookStoreDbContext db,
                IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<ClientsListVM> Handle(GetClientsListQuery request, CancellationToken cancellationToken)
            {
                var clients = await db.Clients
                    .AsNoTracking()
                    .ProjectTo<ClientVO>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new ClientsListVM() { Clients = clients };
            }
        }
    }
}
