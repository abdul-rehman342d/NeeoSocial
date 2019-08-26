using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewSocial.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        [ForeignKey("post")]
        public long PostID { get; set; }
        public long UserID { get; set; }
        public string commentText { get; set; }
        public DateTime commentTime { get; set; }
        public Post post { get; set; }
        public ICollection<SubReaction> SubReactions { get; set; }
    }
}