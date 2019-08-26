using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewSocial.Models
{
    public class SubReaction
    {
        public int SubReactionID { get; set; }

        [ForeignKey("comment")]
        public int CommentID { get; set; }
        public long UserID { get; set; }
        public int reactionType { get; set; }
        public DateTime reactionTime { get; set; }
        public Comment comment { get; set; }
    }
}