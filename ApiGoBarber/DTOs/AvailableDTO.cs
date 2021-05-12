using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.DTOs
{
    public class AvailableDTO
    {
        public string Time { get; set; }

        public DateTime Value { get; set; }

        public bool Available { get; set; }
    }
}
