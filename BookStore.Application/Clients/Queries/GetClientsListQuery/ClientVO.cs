using AutoMapper;
using BookStore.Application.Common.Mappings;
using BookStore.Domain.Entities;

namespace BookStore.Application.Clients.Queries.GetClientsListQuery
{
    public class ClientVO : IMapFrom
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientVO>();
        }
    }
}