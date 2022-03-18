using Confluent.Kafka;
using System.Net;

namespace ShoppingList.API.Producers
{
    public class MessageProducer
    {
        private readonly IConfiguration _configuration;
        private readonly string _host;
        private readonly string _topic;

        public MessageProducer(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _host = configuration.GetSection("Kafka:Host").Value;
            _topic = configuration.GetSection("Kafka:Topic").Value;
        }

        public Task SendMessage(string text)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _host,
                ClientId = Dns.GetHostName()
            };

            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                var message = new Message<string, string>()
                {
                    Key = _topic + Guid.NewGuid(),
                    Value = text,
                    Timestamp = Timestamp.Default
                };

                producer.ProduceAsync(_topic, message);
            }

            return Task.CompletedTask;
        }
    }
}

