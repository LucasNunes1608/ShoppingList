using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TelegramMessaging.API.Application.Commands;
using TelegramMessaging.API.Application.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TelegramMessaging.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageQueries _messageQueries;
        private readonly IMediator _mediator;

        public MessagesController(IMessageQueries messageQueries, IMediator mediator)
        {
            _messageQueries = messageQueries ?? throw new ArgumentNullException(nameof(messageQueries));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("getAll")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Domain.AggregatesModel.Message>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllMessagesAsync()
        {
            try
            {
                var message = await _messageQueries.GetAllMessageAsync();
                return Ok(message);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Route("{messageId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(Domain.AggregatesModel.Message), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMessageAsync(int messageId)
        {
            try
            {
                var message = await _messageQueries.GetMessageAsync(messageId);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Route("saveMessage")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SaveShoppingList([FromBody] SaveMessageCommand command)
        {
            bool commandResult = false;
            commandResult = await _mediator.Send(command);
            if (!commandResult)
                return BadRequest();

            return Ok();
        }

        [Route("removeMessage")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveShoppingList([FromBody] RemoveMessageCommand command)
        {
            bool commandResult = false;
            commandResult = await _mediator.Send(command);
            if (!commandResult)
                return BadRequest();

            return Ok();
        }

    }
}
