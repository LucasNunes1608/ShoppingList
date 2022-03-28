using Confluent.Kafka;
using EventBus.Abstractions;
using EventBus.Events;
using System.Net;
using System.Text.Json;

namespace EventBus.EventBusKafka
{
    public class EventBusKafka : IEventBus
    {
        const string _topic = "testingTopic";
        const string _host = "localhost:9092";
        public async void Publish(IntegrationEvent @event)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _host,
                ClientId = Dns.GetHostName()
            };

            using (var producer = new ProducerBuilder<string, byte[]>(config).Build())
            {
                var message = new Message<string, byte[]>()
                {
                    Key = _topic + Guid.NewGuid(),
                    Value = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType()),
                    Timestamp = Timestamp.Default
                };

                await producer.ProduceAsync(_topic, message);
            }
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            throw new NotImplementedException();
        }
    }
}