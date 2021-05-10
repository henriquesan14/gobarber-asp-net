using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.DTOs
{
    public class AppointmentDTO
    {

        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? CanceledAt { get; set; }

        public ProviderDTO Provider { get; set; }
    }
}
