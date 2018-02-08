using System;

namespace Farfetch.Models
{
    public class Service: DbT
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string ApiKey { get; set; }
    }
}