using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSocial.Models
{
    public class FriendRequest
    {
        public long FriendRequestID { get; set; }
        public long fromReq { get; set; }
        public long toReq { get; set; }
    }
}