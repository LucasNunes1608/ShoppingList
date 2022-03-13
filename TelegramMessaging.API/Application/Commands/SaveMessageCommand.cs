using MediatR;

namespace TelegramMessaging.API.Application.Commands
{
    public class SaveMessageCommand : IRequest<bool>
    {
        public SaveMessageCommand(string messageText, DateTime timeStamp)
        {
            MessageText = messageText;
            TimeStamp = timeStamp;
        }

        public string MessageText { get; private set; }
        public DateTime TimeStamp { get; private set; }
    }
}
