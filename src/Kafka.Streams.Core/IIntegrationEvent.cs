using System;

namespace Kafka.Streams.Core
{
    public interface IIntegrationEvent
    {
        Guid Id { get; }

        DateTime CreationDate { get; }
    }
}
