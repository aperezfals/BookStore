using BookStore.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Clients.Queries.GetClientDetail
{
    public class ClientDetailVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<OrderVO> Orders { get; set; }
    }
}
