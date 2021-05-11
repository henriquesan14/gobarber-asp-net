using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.DTOs
{
    public class NotificationDTO
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }

        public bool Read { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
