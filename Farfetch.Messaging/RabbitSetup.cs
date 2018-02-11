using RabbitMQ.Client;

namespace Farfetch.Messaging
{
    public class RabbitSetup
    {
        protected readonly IModel Channel;

        public RabbitSetup()
        {
            ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };
            IConnection connection = factory.CreateConnection();
            Channel = connection?.CreateModel();
            Channel.ExchangeDeclare("direct_logs", "direct");
        }
    }
}