using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.Books.Queries.GetBooksList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers
{
    public class BookController : BaseController
    {
        [HttpGet("api/Book")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BooksListVM>> GetAll()
        {
            return await Mediator.Send(new GetBooksListQuery());
        }
    }
}
