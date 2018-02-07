using System;
using System.Collections.Generic;

namespace Farfetch.APIHandler.TogglerAPI.DTO
{
    public class ToggleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Value { get; set; }
        public bool Overrides { get; set; }
        public List<ServiceDto> ServiceList { get; set; }
    }
}