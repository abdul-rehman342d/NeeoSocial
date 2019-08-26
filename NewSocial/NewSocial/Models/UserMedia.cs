using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSocial.Models
{
    public class UserMedia
    {
        public long UserMediaID { get; set; }
        public long UserID { get; set; }
        public int type { get; set; }
        public string ImageUrl { get; set; }
    }
}