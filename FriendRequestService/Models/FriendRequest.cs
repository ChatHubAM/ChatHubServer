using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendRequestService.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public string FromRequest { get; set; }
        public string ToRequest { get; set; }
        public bool IsAccepted { get; set; }
    }
}
