using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.DTOs
{
    public class CreateAppointmentDTO
    {

        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public int ProviderId { get; set; }

    }
}
