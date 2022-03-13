using MediatR;
using TelegramMessaging.Domain.AggregatesModel;

namespace TelegramMessaging.API.Application.Commands
{
    public class RemoveMessageCommandHandler : IRequestHandler<RemoveMessageCommand, bool>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMediator _mediator;

        public RemoveMessageCommandHandler(IMessageRepository messageRepository, IMediator mediator)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(RemoveMessageCommand request, CancellationToken cancellationToken)
        {
            var messageToRemove = await _messageRepository.GetAsync(request.Id);
            if (messageToRemove == null)
            {
                return false;
            }

            _messageRepository.Remove(messageToRemove);

            return await _messageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
