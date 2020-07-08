using BookStore.Application.Clients.Commands.DeleteClient;
using BookStore.Application.Clients.Commands.UpsertClient;
using BookStore.Application.Clients.Queries.GetClientDetail;
using BookStore.Application.Clients.Queries.GetClientsListQuery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebUI.Controllers
{
    public class ClientController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ClientsListVM>> GetAll()
        {
            return await Mediator.Send(new GetClientsListQuery());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ClientDetailVM>> Get(int id)
        {
            return await Mediator.Send(new GetClientDetailQuery() { Id = id });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> Post([FromBody] UpsertClientCommand command)
        {
            int id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteClientCommand() { Id = id });
            return NoContent();
        }

    }
}
