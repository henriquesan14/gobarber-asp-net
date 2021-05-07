using ApiGoBarber.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Entities
{
    public class Appointment : Entity
    {

        public DateTime Date { get; set; }

        public DateTime CanceledAt { get; set; }

        public virtual bool Past { get; set; }

        public bool Cancelable { get; set; }

        public virtual User User { get; set; }

        public User Provider { get; set; }
    }
}
