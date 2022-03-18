using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public record IntegrationEvent
    {
        [JsonInclude]
        public Guid Id { get; init; }
        [JsonInclude]
        public DateTime CreationTime { get; init; }
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.UtcNow;
        }
        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime creationTime)
        {
            Id = id;
            CreationTime = creationTime;
        }
    }
}
