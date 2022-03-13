using TelegramMessaging.Domain.AggregatesModel;

namespace TelegramMessaging.API.Application.Queries
{
    public interface IMessageQueries
    {
        public Task<Message> GetMessageAsync(int id);
        public Task<IEnumerable<Message>> GetAllMessageAsync();
    }
}
