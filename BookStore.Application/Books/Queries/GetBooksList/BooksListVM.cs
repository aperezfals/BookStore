using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Books.Queries.GetBooksList
{
    public class BooksListVM
    {
        public IList<BookLookupDTO> Books { get; set; }
    }
}
