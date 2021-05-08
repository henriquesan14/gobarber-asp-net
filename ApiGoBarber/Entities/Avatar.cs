using ApiGoBarber.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Entities
{
    public class Avatar : Entity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }

    }
}
