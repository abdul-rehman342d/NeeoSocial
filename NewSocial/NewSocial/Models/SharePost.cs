using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewSocial.Models
{
    public class SharePost
    {
        public long SharePostID { get; set; }

        [ForeignKey("post")]
        public long PostID { get; set; }
        public long UserID { get; set; }
        public string text { get; set; }
        public DateTime shareTime { get; set; }
        public DateTime updateTime { get; set; }
        public Post post { get; set; }
    }
}