using BookStore.Application.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.WebUI.Controllers
{
    public class OrderController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> Post([FromBody] CreateOrderCommand command)
        {
            int id = await Mediator.Send(command);
            return Ok(id);
        }
    }
}
