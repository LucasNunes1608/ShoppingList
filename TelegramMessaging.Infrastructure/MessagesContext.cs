using Microsoft.EntityFrameworkCore;
using TelegramMessaging.Domain.AggregatesModel;
using TelegramMessaging.Domain.SeedWork;

namespace TelegramMessaging.Infrastructure
{
    public class MessagesContext : DbContext, IUnitOfWork
    {
        public DbSet<Message> messages { get; set; }
        public MessagesContext(DbContextOptions options) : base(options)
        {
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}