using System;
using System.Threading.Tasks;
using sunny_dn_01.Domains;

namespace sunny_dn_01.Service.KafkaService
{
    public interface IKafPublisher
    {
        Task PublishAsync(string topic, string msg);
    }
}
