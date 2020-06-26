using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Books.Queries.GetBookDetail
{
    public class GetBookDetailQueryHandler : IRequestHandler<GetBookDetailQuery, BookDetailVM>
    {
        public Task<BookDetailVM> Handle(GetBookDetailQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
