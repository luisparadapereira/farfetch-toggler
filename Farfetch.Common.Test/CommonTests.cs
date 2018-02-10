using System;
using System.IO;
using Farfetch.APIHandler.Common;
using Farfetch.DB;
using Farfetch.PlusApp;
using Farfetch.Tests;
using NUnit.Framework;

namespace Farfetch.Common.Test
{
    [TestFixture]
    public class CommonTests: TestBase
    {
        [Test]
        public void Should_ThrowException_When_EmptyFile()
        {
            const string filePath = "";
            DbSettings output;

            try
            {
                SettingsReader<DbSettings> settingsReader = new SettingsReader<DbSettings>();
                output = settingsReader.Read(filePath);
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void Should_ReadCorrectDbSettings()
        {
            const string filePath = @"dbsettings.json";
            DbSettings output = null;

            try
            {
                SettingsReader<DbSettings> settingsReader = new SettingsReader<DbSettings>();
                output = settingsReader.Read(filePath);
            }
            catch (ArgumentNullException)
            {
                Assert.Fail();
            }

            Assert.NotNull(output);
            Assert.IsNotEmpty(output.Database);
            Assert.IsNotEmpty(output.DatabaseName);
            Assert.IsNotEmpty(output.Port);
            Assert.IsNotEmpty(output.Server);
        }

        [Test]
        public void Should_ReadCorrectApiSettings()
        {
            const string filePath = @"API_Toggler/restSettings.json";
            ApiSettings output = null;

            try
            {
                SettingsReader<ApiSettings> settingsReader = new SettingsReader<ApiSettings>();
                output = settingsReader.Read(filePath);
            }
            catch (ArgumentNullException)
            {
                Assert.Fail();
            }

            Assert.NotNull(output);
            Assert.IsNotEmpty(output.Version);
            Assert.IsNotEmpty(output.Protocol);
            Assert.IsNotEmpty(output.Server);
        }

        [Test]
        public void Should_ThrowException_When_FileDoesntExist()
        {
            const string filePath = @"doesntExist.json";

            try
            {
                SettingsReader<string> settingsReader = new SettingsReader<string>();
                settingsReader.Read(filePath);
            }
            catch (FileNotFoundException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void Should_ReadAssemblyInformation()
        {
            const string assemblyName = "Farfetch.PlusApp";
            const string assemblyVersion = "1.0.0.0";

            Number numberApplication = new Number();
            string numberAssemblyName = numberApplication.GetAssemblyName();
            string numberAssemblyVersion = numberApplication.GetAssemblyVersion();

            Assert.AreEqual(assemblyName, numberAssemblyName);
            Assert.AreEqual(assemblyVersion, numberAssemblyVersion);

        }
    }
}
