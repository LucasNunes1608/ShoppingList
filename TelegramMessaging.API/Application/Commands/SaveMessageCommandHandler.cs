using MediatR;
using TelegramMessaging.Domain.AggregatesModel;

namespace TelegramMessaging.API.Application.Commands
{
    public class SaveMessageCommandHandler : IRequestHandler<SaveMessageCommand, bool>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMediator _mediator;

        public SaveMessageCommandHandler(IMessageRepository messageRepository, IMediator mediator)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(SaveMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message(request.MessageText, request.TimeStamp);
            _messageRepository.Add(message);
            return await _messageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
