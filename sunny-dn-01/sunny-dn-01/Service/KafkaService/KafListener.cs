using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kafka.Public;
using MediatR;
using Microsoft.Extensions.Hosting;
using sunny_dn_01.Domains;
using sunny_dn_01.Service.UserService;

namespace sunny_dn_01.Service.KafkaService
{
    public class KafListener: IKafListener, IHostedService
    {

        private readonly IMediator _mediator;
        private readonly ConsumerConfig _config;
        
        public KafListener(IMediator mediator)
        {
            _mediator = mediator;
            _config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "foo"
            };
        }

        public void Listen()
        {
            /*
            var config = new Dictionary<string, object>
            {
                {"bootstrap.servers", "localhost:9092" }
            };*/

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var consumer = new ConsumerBuilder<Ignore, string>(this._config).Build())
            {
                consumer.Subscribe("create-user");
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume(cancellationToken);
                    Console.WriteLine(consumeResult.Message);
                    Console.WriteLine(consumeResult.Message.Value);
                    _mediator.Send(new CreateUserCommand
                    {
                        User = new User { Email = "aaa@456.com" }
                    });
                }
                consumer.Close();
                return Task.CompletedTask;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            
            return Task.CompletedTask;
        }
    }
}
