using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.API.Application.Commands;
using ShoppingList.API.Application.Queries;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingList.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingListsController : ControllerBase
    {
        private readonly IShoppingListQueries _shoppingListQueries;
        private readonly IMediator _mediator;
        public ShoppingListsController(IShoppingListQueries shoppingListQueries,
                                        IMediator mediator)
        {
            _shoppingListQueries = shoppingListQueries ?? throw new ArgumentNullException(nameof(shoppingListQueries));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("{ShoppingListId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(Domain.AggregatesModel.ShoppingList), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetShoppingListAsync(int ShoppingListId)
        {
            try
            {
                var shoppingList = await _shoppingListQueries.GetShoppingListAsync(ShoppingListId);
                return Ok(shoppingList);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        [Route("saveList")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult>SaveShoppingList([FromBody] SaveShoppingListCommand command)
        {
            bool commandResult = false;
            commandResult = await _mediator.Send(command);
            if (!commandResult)
                return BadRequest();

            return Ok();
        }

        [Route("removeList")]
        [HttpPost]
        [ProducesResponseType ((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult>RemoveShoppingList([FromBody] RemoveShoppingListCommand command)
        {
            bool commandResult = false;
            commandResult = await _mediator.Send(command);
            if (!commandResult)
                return BadRequest();

            return Ok();
        }

        [Route("updateList")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateShoppingList([FromBody] UpdateShoppingListCommand command)
        {
            bool commandResult = false;
            commandResult = await _mediator.Send(command);
            if (!commandResult)
                return BadRequest();

            return Ok();
        }
    }
}
