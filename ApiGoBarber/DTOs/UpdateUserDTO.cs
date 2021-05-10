using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.DTOs
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public string OldPassword { get; set; }

        public string Password { get; set; }
        public int AvatarId { get; set; }

    }
}
