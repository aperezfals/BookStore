using BookStore.Domain.Common;
using BookStore.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Entities
{
    public class Book : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ISBN ISBN { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
