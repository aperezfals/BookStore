using AutoMapper;
using BookStore.Application.Common.Mappings;
using BookStore.Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace BookStore.Application.Books.Queries.GetBooksList
{
    public class BookLookupDTO : IMapFrom
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public AuthorDTO Author { get; set; }

        public string ISBN { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, BookLookupDTO>();
        }
    }
}