using System.Text;
using RabbitMQ.Client;

namespace Ferfetch.Messaging
{
    /// <summary>
    /// Sends a message via RabbitMQ
    /// </summary>
    public class Broadcaster: RabbitSetup
    {
        /// <summary>
        /// Sends a specific message to a specific channel
        /// </summary>
        /// <param name="msgChannel">The channel to broadcast</param>
        /// <param name="message">The message to send</param>
        public void SendMessage(string msgChannel, string message)
        {
            var body = Encoding.UTF8?.GetBytes(message);
            Channel.BasicPublish(
                "direct_logs",
                msgChannel,
                null,
                body
            );
        }

    }
}