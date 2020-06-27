using BookStore.Application.Clients.Commands.UpsertClient;
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> Post([FromBody] UpsertClientCommand command)
        {
            int id = await Mediator.Send(command);
            return Ok(id);
        }
    }
}
