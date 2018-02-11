using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Farfetch.Messaging
{
    /// <summary>
    /// Subscribes to a message via RabbitMQ
    /// </summary>
    public class Subscriber: RabbitSetup
    {
        /// <summary>
        /// Receives a message via a specific channel
        /// </summary>
        /// <param name="msgChannel">The channel to listen to</param>
        /// <param name="delegateHandler">The eventHandler to execute when the message is received</param>
        public void ReceiveMessage(string msgChannel, FarfetchDelegate delegateHandler)
        {
            var queueName = Channel?.QueueDeclare()?.QueueName;

            Channel.QueueBind( queueName, "direct_logs", msgChannel);

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;

                var message = Encoding.UTF8.GetString(body);

                delegateHandler.Invoke(message);

            };
            Channel.BasicConsume(queueName, true, consumer);
        }
    }
}