using Confluent.Kafka;
using MediatR;
using TelegramMessaging.API.Application.Commands;

namespace TelegramMessaging.API.Consumer
{
    public class MessageConsumer : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly string _topic;
        private readonly string _host;
        public MessageConsumer(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _host = configuration.GetSection("Kafka:Host").Value;
            _topic = configuration.GetSection("Kafka:Topic").Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = $"{_topic}-group-0",
                BootstrapServers = _host,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            try
            {
                using (var consumer = new ConsumerBuilder<string, string>(config).Build())
                {
                    consumer.Subscribe(_topic);

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var cr = consumer.Consume(cancellationToken);
                        _mediator.Send(new SaveMessageCommand(cr.Message.Value, cr.Message.Timestamp.UtcDateTime));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
