using Farfetch.ServiceManager.Interfaces;
using Farfetch.Tests;
using NUnit.Framework;

namespace Farfetch.ServiceFactory.Test
{
    [TestFixture]
    public class ServiceFactoryTests: TestBase
    {
        [Test]
        public void Should_RetriveServicesCorrectly()
        {
            Factory factory = new Factory(FILE_SETTINGS_PATH);
            IService togglerService = factory.GetDbService(AvailableServices.Toggler);
            Assert.NotNull(togglerService);

            IService togglerApplication = factory.GetDbService(AvailableServices.TogglerApplication);
            Assert.NotNull(togglerApplication);

            IService userAccountsService = factory.GetDbService(AvailableServices.UserAccounts);
            Assert.NotNull(userAccountsService);
        }
    }
}
