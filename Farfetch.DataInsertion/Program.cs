using System;
using System.Collections.Generic;
using Farfetch.CoreUnitOfWork;
using Farfetch.Models;

namespace Farfetch.DataInsertion
{
    class Program
    {
        static void Main(string[] args)
        {
            string settingsFilePath = "dbsettings.json";
            CoreUnit<User> userCore = new CoreUnit<User>(settingsFilePath);
            if (userCore == null) throw new NullReferenceException("core isn't initialized");
            if (userCore.Repository == null) throw new NullReferenceException("repository isn't initialized");

            User admin = new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Password = "admin",
                Profile = "Admin"
            };

            User developer = new User
            {
                Id = Guid.NewGuid(),
                Username = "developer",
                Password = "developer",
                Profile = "Developer"
            };

            User publicUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "public",
                Password = "public",
                Profile = "Public"
            };
            userCore.Repository.Insert(admin);
            userCore.Repository.Insert(developer);
            userCore.Repository.Insert(publicUser);

            CoreUnit<Service> serviceCore = new CoreUnit<Service>(settingsFilePath);
            if (serviceCore == null) throw new NullReferenceException("core isn't initialized");
            if (serviceCore.Repository == null) throw new NullReferenceException("repository isn't initialized");

            Service service = new Service
            {
                Id = Guid.NewGuid(),
                Name = "Farfetch.PlusApp",
                Version = "1.0.0.0",
                ApiKey =
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTZXJ2aWNlIiwiU2VydmljZU5hbWUiOiJGYXJmZXRjaC5QbHVzQXBwIiwiU2VydmljZVZlcnNpb24iOiIxLjAuMC4wIiwiZXhwIjoxNTQ5NzQxMDQ0LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjU3MTQ2IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1NzE0NiJ9.KfJA_lTVKCbSVmliY_jZ98YyCfCh5g-ioBiNFmdUdgg"
            };

            serviceCore.Repository.Insert(service);

            CoreUnit<Toggle> toggleCore = new CoreUnit<Toggle>(settingsFilePath);
            if (toggleCore == null) throw new NullReferenceException("core isn't initialized");
            if (toggleCore.Repository == null) throw new NullReferenceException("repository isn't initialized");

            Toggle toggle1true = new Toggle
            {
                Id = Guid.NewGuid(),
                Name = "toggle1",
                Value = true,
                Overrides = false,
                ServiceList = new List<Service>
                {
                    service
                }
            };

            Toggle toggle1false = new Toggle
            {
                Id = Guid.NewGuid(),
                Name = "toggle1",
                Value = false,
                Overrides = true,
                ServiceList = new List<Service>
                {
                    service
                }
            };

            Toggle toggle2true = new Toggle
            {
                Id = Guid.NewGuid(),
                Name = "toggle2",
                Value = true,
                Overrides = false,
                ServiceList = new List<Service>
                {
                    service
                }
            };

            toggleCore.Repository.Insert(toggle1true);
            toggleCore.Repository.Insert(toggle1false);
            toggleCore.Repository.Insert(toggle2true);

        }
    }
}
