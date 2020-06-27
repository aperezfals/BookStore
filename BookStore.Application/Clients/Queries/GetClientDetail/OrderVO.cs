using System.Security.Principal;

namespace BookStore.Application.Clients.Queries.GetClientDetail
{
    public class OrderVO
    {
        public string BookName { get; set; }

        public int BookId { get; set; }

        public string AuthorFullName { get; set; }

        public int AuthorId { get; set; }

        public int Ammount { get; set; }
    }
}