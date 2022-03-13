using MediatR;

namespace TelegramMessaging.API.Application.Commands
{
    public class RemoveMessageCommand : IRequest<bool>
    {
        public int Id { get; private set; }
        public RemoveMessageCommand(int id)
        {
            Id = id;
        }
    }
}
