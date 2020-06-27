using BookStore.Application.Clients.Commands.UpsertClient;
using BookStore.Application.Clients.Queries.GetClientsListQuery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Controllers
{
    public class ClientController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ClientsListVM>> GetAll()
        {
            return await Mediator.Send(new GetClientsListQuery());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> Post([FromBody] UpsertClientCommand command)
        {
            int id = await Mediator.Send(command);
            return Ok(id);
        }
    }
}
