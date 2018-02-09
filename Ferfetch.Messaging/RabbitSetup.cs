using RabbitMQ.Client;

namespace Ferfetch.Messaging
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