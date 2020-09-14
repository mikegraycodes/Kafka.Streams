using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Streams.Core
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IConsumer consumer;
        private readonly ILogger<KafkaConsumerService> logger;

        public KafkaConsumerService(IConsumer consumer, ILogger<KafkaConsumerService> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("OrderProcessing Service Started");

            while (!cancellationToken.IsCancellationRequested)
            {
                var message = consumer.Read(cancellationToken);

                logger.LogInformation($"Proccessed '{message}'.");
            }
        }
    }
}
