namespace BookStore.Application.Books.Queries.GetBookDetail
{
    public class BookDetailVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public AuthorDTO Author { get; set; }

        public string ISBN { get; set; }
    }
}