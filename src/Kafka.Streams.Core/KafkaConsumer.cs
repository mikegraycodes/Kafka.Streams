using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Kafka.Streams.Core
{
    public class KafkaConsumer : IConsumer, IDisposable
    {
        private readonly IConsumer<Ignore, string> consumer;
        private readonly ILogger<KafkaConsumer> logger;

        public KafkaConsumer(ConsumerConfigWrapper consumerConfig, ILogger<KafkaConsumer> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var conf = new ConsumerConfig
            {
                GroupId = consumerConfig.GroupId,
                BootstrapServers = consumerConfig.BootstrapServers,
                AutoOffsetReset = consumerConfig.AutoOffsetReset
            };


            consumer = new ConsumerBuilder<Ignore, string>(conf).Build();
            consumer.Subscribe(consumerConfig.Topic);
        }


        public string Read(CancellationToken cancellationToken = default)
        {
            try
            {
                var cr = consumer.Consume(cancellationToken);
                logger.LogInformation($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                return cr.Message.Value;
            }
            catch (ConsumeException e)
            {
                logger.LogWarning($"Error occured: {e.Error.Reason}");
                throw;
            }
        }


        public void Dispose()
        {
            consumer.Dispose();
        }
    }
}
