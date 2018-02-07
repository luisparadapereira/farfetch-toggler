using System;
using System.Collections.Generic;
using Farfetch.CoreUnitOfWork;
using Farfetch.Models;
using Farfetch.PlusApp;
using Farfetch.Repositories.MongoDB;
using Farfetch.ServiceFactory;
using Farfetch.Toggler.Service;

namespace TODELETE_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Number plusApp = new Number();
            //int feh = plusApp.CalcNumber(4);
            //int a = 2;

            //Factory factory = new Factory();
            //TogglerService service = factory.GetDbService("togglerService") as TogglerService;

            //Toggle toggle = new Toggle()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "toggle1",
            //    Value = true,
            //    Overrides = false,
            //    ServiceList = new List<Service>
            //    {
            //        new Service
            //        {
            //            Name = "service1",
            //            Version = "1.0.0"
            //        },
            //        new Service
            //        {
            //            Name = "service2",
            //            Version = "1.1.0"
            //        },
            //        new Service
            //        {
            //            Name = "service3",
            //            Version = "1.0.2"
            //        },
            //    }
            //};
            //service.Insert(toggle);

            //toggle = new Toggle()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "toggle1",
            //    Value = false,
            //    Overrides = true,
            //    ServiceList = new List<Service>
            //    {
            //        new Service
            //        {
            //            Name = "service1",
            //            Version = "1.0.0"
            //        },
            //        new Service
            //        {
            //            Name = "service4",
            //            Version = "0.0.1"
            //        }
            //    }
            //};
            //service.Insert(toggle);

            //toggle = new Toggle()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "toggle2",
            //    Value = true,
            //    Overrides = false,
            //    ServiceList = new List<Service>
            //    {
            //        new Service
            //        {
            //            Name = "service3",
            //            Version = "1.0.2"
            //        },
            //    }
            //};
            //service.Insert(toggle);



            //    var kaboom = service.GetBy(x => x.ServiceList.Contains("feh"));




            //Console.ReadLine();

            User publicUser = new User
            {
                Id = Guid.Empty,
                Password = "user",
                Username = "common",
                Profile = "Public"
            };

            CoreUnit<User> core = new CoreUnit<User>();
            core.Repository.Insert(publicUser);

        }
    }
}
