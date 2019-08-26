using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSocial.Models
{
    public class Post
    {
        public long PostID { get; set; }
        public long UserID { get; set; }
        public string text { get; set; }
        public string imageURL { get; set; }
        public byte[] video { get; set; }
        public DateTime postTime { get; set; }
        public DateTime updateTime { get; set; }


        public ICollection<SharePost> SharePosts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
        public ICollection<PostImage> PostImages { get; set; }


    }
}