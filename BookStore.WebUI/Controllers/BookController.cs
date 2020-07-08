using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Application.Books.Commands.DeleteBook;
using BookStore.Application.Books.Commands.UpsertBook;
using BookStore.Application.Books.Queries.GetBookDetail;
using BookStore.Application.Books.Queries.GetBooksList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class BookController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BooksListVM>> GetAll()
        {
            var l = await Mediator.Send(new GetBooksListQuery());
            return Ok(l);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDetailVM>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetBookDetailQuery() { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> Post([FromBody]UpsertBookCommand command)
        {
            int id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteBookCommand() { Id = id });
            return NoContent();
        }
    }
}
