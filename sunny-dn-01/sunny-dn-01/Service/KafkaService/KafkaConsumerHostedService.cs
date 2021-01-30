using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Public;
using Kafka.Public.Loggers;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace sunny_dn_01.Service.KafkaService
{
    public class KafkaConsumerHostedService : IHostedService, IKafListener
    {
        private readonly ILogger<KafkaConsumerHostedService> _logger;
        private readonly ClusterClient _cluster;
        private IMediator _mediator;


        public KafkaConsumerHostedService(ILogger<KafkaConsumerHostedService> logger,
            IMediator mediator)//IDbContextFactory<AppDbContext> fac)
        {

            _logger = logger;
            _mediator = mediator;

            _cluster = new ClusterClient(new Configuration
            {
                Seeds = "localhost:9092"
            }, new ConsoleLogger());
        }

        public void Listen()
        {
            Console.WriteLine("to do");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cluster.ConsumeFromLatest("create-user");
            _cluster.MessageReceived += record =>
            {
                string email = Encoding.UTF8.GetString(record.Value as byte[]);
                _logger.LogInformation($"Received: {email}");
            };

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cluster?.Dispose();
            return Task.CompletedTask;
        }
    }



    /*
    public class KafkaProducerHostedService : IHostedService
    {
        private readonly ILogger<KafkaProducerHostedService> _logger;
        private IProducer<Null, string> _producer;

        public KafkaProducerHostedService(ILogger<KafkaProducerHostedService> logger)
        {
            _logger = logger;
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _producer = new ProducerBuilder<Null, string>(config).Build();


        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            for (var i = 0; i < 1; ++i)
            {
                try
                {
                    var dr = await _producer.ProduceAsync("new-user", new Message<Null, string> { Value = $"newUser {i}" }, cancellationToken);
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.Partition}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
            _producer.Flush(TimeSpan.FromSeconds(5));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _producer?.Dispose();
            return Task.CompletedTask;
        }
    }
    */
}
