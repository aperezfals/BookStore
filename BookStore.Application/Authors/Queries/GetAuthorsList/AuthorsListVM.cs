using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Authors.Queries.GetAuthorsList
{
    public class AuthorsListVM 
    {
        public IList<AuthorDTO> Authors { get; set; }
    }
}
