using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Kafka.Streams.Core
{
    public class KafkaProducer : IProducer, IDisposable
    {
        private readonly IProducer<Null, string> producer;
        private readonly ILogger<KafkaProducer> logger;

        public KafkaProducer(ILogger<KafkaProducer> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var config = new ProducerConfig { BootstrapServers = "kafka:9092" };
            producer = new ProducerBuilder<Null, string>(config).Build();
        }


        public async Task Publish(IIntegrationEvent @event, string topic)
        {
            try
            {
                var eventName = @event.GetType().Name;

                var message = JsonConvert.SerializeObject(@event);

                var dr = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });

                logger.LogInformation($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                logger.LogWarning($"Delivery failed: {e.Error.Reason}");
            }
        }

        public void Dispose()
        {
            producer.Dispose();
        }
    }
}
