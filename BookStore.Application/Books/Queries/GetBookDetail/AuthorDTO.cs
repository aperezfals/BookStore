using AutoMapper;
using BookStore.Application.Common.Mappings;
using BookStore.Domain.Entities;

namespace BookStore.Application.Books.Queries.GetBookDetail
{
    public class AuthorDTO : IMapFrom
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Author, AuthorDTO>();
        }
    }
}
