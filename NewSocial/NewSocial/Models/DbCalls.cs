using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewSocial.Models
{
    public class DbCalls : DbContext
    {
        public DbCalls() : base("Connection")
        {
        }
        
        public DbSet<User> User { get; set; }
        public DbSet<UserMedia> UserMedia { get; set; }
        //Post
        public DbSet<Post> Post { get; set; }
        public DbSet<SharePost> SharePost { get; set; }
        public DbSet<PostImage> PostImage { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Reaction> Reaction { get; set; }
        public DbSet<SubReaction> SubReaction { get; set; }
        //Friend
        public DbSet<FriendRequest> FriendRequest { get; set; }
        public DbSet<Friend> Friend { get; set; }

    }
}