using AutoMapper;
using BookStore.Application.Common.Mappings;
using BookStore.Domain.Entities;

namespace BookStore.Application.Books.Queries.GetBookDetail
{
    public class BookDetailVM : IMapFrom
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AuthorId { get; set; }

        public AuthorDTO Author { get; set; }

        public string ISBN { get; set; }

        public int OrdersAmmount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, BookDetailVM>();
        }
    }
}