using System;

namespace Farfetch.APIHandler.TogglerAPI.DTO
{
    public class ToggleListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Value { get; set; }

    }
}