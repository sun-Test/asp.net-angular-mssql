using System;
using System.Threading.Tasks;
using System.Threading;
using Confluent.Kafka;
using sunny_dn_01.Domains;

namespace sunny_dn_01.Service.KafkaService
{
    public class KafPublisher : IKafPublisher
    {
        private IProducer<Null, string> _publisher;

        public KafPublisher()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _publisher = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task PublishAsync(string topic, string msg)
        {

            try
            {
                var dr = await _publisher.ProduceAsync(topic, new Message<Null, string> { Value = msg });
                Console.WriteLine($"Delivered '{dr.Value}' to '{dr.Partition}'");
                _publisher.Flush(TimeSpan.FromSeconds(1));
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
            
        }
    }
}
