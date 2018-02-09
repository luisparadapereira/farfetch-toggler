using System;

namespace Farfetch.APIHandler.TogglerAPI.DTO
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string ApiKey { get; set; }
    }
}