using System.Threading;

namespace Kafka.Streams.Core
{
    public interface IConsumer
    {
        string Read(CancellationToken cancellationToken = default);
    }
}
