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

        public bool Past { 
            get
            {
                return Date < DateTime.Now;
            } 
        }

        public bool Cancelable
        {
            get
            {
                return DateTime.Now < Date.Value.AddHours(-2);
            }
        }
    }
}
