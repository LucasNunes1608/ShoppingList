using Dapper;
using System.Data.SqlClient;

namespace TelegramMessaging.API.Application.Queries
{
    public class MessageQueries : IMessageQueries
    {
        private readonly string _connectionString;

        public MessageQueries(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<Message>> GetAllMessageAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(
                    @"select Id as Id, messageText as messagetext, timeStamp as timestamp
                        FROM Message"
                    );
                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();
                return MapItems(result);
            }
        }

        public async Task<Message> GetMessageAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(
                    @"select Id as Id, messageText as messagetext, timeStamp as timestamp
                        FROM Message
                        Where Id=@id",
                    new { id }
                    );
                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return MapItem(result);
            }
        }

        private Message MapItem(dynamic result)
        {
            var message = new Message()
            {
                Id = result.Id,
                messagetext = result.messagetext,
                timestamp = result.timestamp,
            };
            return message;
        }

        private IEnumerable<Message> MapItems(dynamic result)
        {
            var messages = new List<Message>();
            foreach (dynamic item in result)
            {
                var message = new Message()
                {
                    Id = item.Id,
                    messagetext = item.messagetext,
                    timestamp = item.timestamp
                };

                messages.Add(message);
            }
            return messages;
        }
    }
}
