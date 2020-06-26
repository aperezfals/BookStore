using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
