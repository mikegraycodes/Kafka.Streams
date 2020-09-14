using System.Threading.Tasks;

namespace Kafka.Streams.Core
{
    public interface IProducer
    {
        Task Publish(IIntegrationEvent @event, string topic);
    }
}
