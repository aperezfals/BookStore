using BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public int Ammount { get; set; }
    }
}
