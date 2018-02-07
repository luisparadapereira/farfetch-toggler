using System.Collections.Generic;

namespace Farfetch.Models
{
    public class Toggle: DbT
    {
        public string Name { get; set; }
        public bool Value { get; set; }
        public bool Overrides { get; set; }
        public List<Service> ServiceList { get; set; }
    }
}