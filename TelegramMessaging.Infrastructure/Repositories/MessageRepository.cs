using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramMessaging.Domain.AggregatesModel;
using TelegramMessaging.Domain.SeedWork;

namespace TelegramMessaging.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessagesContext _context;

        public MessageRepository(MessagesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Message Add(Message message)
        {
            return _context.messages.Add(message).Entity;
        }

        public async Task<Message> GetAsync(int messageId)
        {
            var message = await _context
                                    .messages
                                    .FirstOrDefaultAsync(m => m.Id == messageId);
            if(message == null)
            {
                message = _context
                            .messages
                            .Local
                            .FirstOrDefault(m => m.Id == messageId);
            }

            return message;
        }

        public void Remove(Message message)
        {
            _context.Entry(message).State = EntityState.Deleted;
        }

        public void Update(Message message)
        {
            _context.Entry(message).State = EntityState.Modified;
        }
    }
}
