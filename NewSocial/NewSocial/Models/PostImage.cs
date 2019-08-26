using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewSocial.Models
{
    public class PostImage
    {
        public int PostImageID { get; set; }

        [ForeignKey("post")]
        public long PostID { get; set; }
        public string imagePath { get; set; }
        public Post post { get; set; }
    }
}