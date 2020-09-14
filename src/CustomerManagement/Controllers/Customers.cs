using Kafka.Streams.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        private readonly ILogger<CustomersController> logger;
        private readonly IProducer producer;

        public CustomersController(ILogger<CustomersController> logger, IProducer producer)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            producer.Publish(new IntegrationEvent(), "customer");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
