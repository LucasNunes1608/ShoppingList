using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramMessaging.Domain.SeedWork;

namespace TelegramMessaging.Domain.AggregatesModel
{
    public interface IMessageRepository : IRepository<Message>
    {
        Message Add(Message message);

        void Update(Message message);

        Task<Message> GetAsync(int messageId);
        void Remove(Message message);
    }
}
