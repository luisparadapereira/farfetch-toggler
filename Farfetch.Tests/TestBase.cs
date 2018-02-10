using Farfetch.Automapper;
using NUnit.Framework;

namespace Farfetch.Tests
{
    public class TestBase
    {
        [OneTimeSetUp]
        public void Startup()
        {
            Initializer automapperInit = new Initializer();
            automapperInit.RegisterMappings();
        }
    }
}
