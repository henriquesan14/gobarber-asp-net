using ApiGoBarber.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordHash { get; set; }

        public bool Provider { get; set; }

        public File File { get; set; }
    }
}
