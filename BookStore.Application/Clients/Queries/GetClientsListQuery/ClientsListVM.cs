using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Clients.Queries.GetClientsListQuery
{
    public class ClientsListVM
    {
        public IList<ClientVO> Clients { get; set; }
    }
}
