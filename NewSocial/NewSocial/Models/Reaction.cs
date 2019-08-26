using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewSocial.Models
{
    public class Reaction
    {
        public int ReactionID { get; set; }

        [ForeignKey("post")]
        public long PostID { get; set; }
        public long UserID { get; set; }
        public int reactionType { get; set; }
        public DateTime reactionTime { get; set; }
        public Post post { get; set; }
    }
}