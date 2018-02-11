using Farfetch.Automapper;
using NUnit.Framework;

namespace Farfetch.Tests
{
    public class TestBase
    {
        protected const string FILE_SETTINGS_PATH = @"dbsettings_test.json";

        [OneTimeSetUp]
        public void Startup()
        {
            Initializer automapperInit = new Initializer();
            automapperInit.RegisterMappings();
        }
    }
}
