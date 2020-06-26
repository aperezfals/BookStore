using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Books.Queries.GetBookDetail
{
    public class GetBookDetailQuery : IRequest<BookDetailVM>
    {
    }
}
