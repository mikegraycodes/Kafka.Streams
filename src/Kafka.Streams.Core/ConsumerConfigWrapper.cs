using Confluent.Kafka;

namespace Kafka.Streams.Core
{
    public class ConsumerConfigWrapper
    {
        public string GroupId { get; set; }
        public string BootstrapServers { get; set; }
        public AutoOffsetReset AutoOffsetReset { get; set; }
        public string Topic { get; set; }
    }
}
