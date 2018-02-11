using NUnit.Framework;

namespace Farfetch.Messaging.Test
{
    [TestFixture]
    public class MessagingTests
    {
        [Test]
        public void Should_Create_Broadcaster()
        {
            Broadcaster broadcaster = CreateBroadcaster();
            Assert.NotNull(broadcaster);
        }

        [Test]
        public void Should_Create_Subscriber()
        {
            Subscriber subscriber = CreateSubscriber();
            Assert.NotNull(subscriber);
        }

        private Broadcaster CreateBroadcaster()
        {
            Broadcaster broadcaster = new Broadcaster();
            return broadcaster;
        }

        private Subscriber CreateSubscriber()
        {
            Subscriber subscriber = new Subscriber();
            return subscriber;
        }
    }
}
