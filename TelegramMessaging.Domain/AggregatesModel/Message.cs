using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramMessaging.Domain.SeedWork;

namespace TelegramMessaging.Domain.AggregatesModel
{
    public class Message : Entity, IAggregateRoot
    {
        public string messageText { get; private set; }
        public DateTime timeStamp { get; private set; }
        public Message(string messageText, DateTime timeStamp)
        {
            this.messageText = messageText;
            this.timeStamp = timeStamp;
        }
    }
}
