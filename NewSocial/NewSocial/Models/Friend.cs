using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSocial.Models
{
    public class Friend
    {
        public long FriendID { get; set; }
        public long UserID1 { get; set; }
        public long UserID2 { get; set; }
    }
}