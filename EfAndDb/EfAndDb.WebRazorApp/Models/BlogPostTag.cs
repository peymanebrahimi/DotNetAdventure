using System;
using System.Collections.Generic;

namespace EfAndDb.WebRazorApp.Models
{
    // one to one relation of blog and blogImage
    // one to many relation of blog and post
    // many to many relation of post and tag
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public BlogImage BlogImage { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class BlogImage
    {
        public int BlogImageId { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public List<PostTag> PostTags { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
        public List<PostTag> PostTags { get; set; }
    }

    public class PostTag
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}