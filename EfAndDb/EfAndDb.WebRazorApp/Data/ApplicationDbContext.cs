using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EfAndDb.WebRazorApp.Models;

namespace EfAndDb.WebRazorApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.Posts)
                .UsingEntity<PostTag>(
                    j => j
                        .HasOne(pt => pt.Tag)
                        .WithMany(t => t.PostTags)
                        .HasForeignKey(pt => pt.TagId),
                    j => j
                        .HasOne(pt => pt.Post)
                        .WithMany(p => p.PostTags)
                        .HasForeignKey(pt => pt.PostId),
                    j =>
                    {
                        //j.Property(pt => pt.PublicationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                        j.HasKey(t => new { t.PostId, t.TagId });
                    });

            //builder.Entity<PostTag>()
            //    .HasKey(x => new { x.PostId, x.TagId });

            //builder.Entity<PostTag>()
            //    .HasOne<Post>(x => x.Post)
            //    .WithMany(x => x.PostTags)
            //    .HasForeignKey(x => x.PostId);

            //builder.Entity<PostTag>()
            //    .HasOne<Tag>(x => x.Tag)
            //    .WithMany(x => x.PostTags)
            //    .HasForeignKey(x => x.TagId);

            builder.Entity<Blog>().HasData(new Blog { BlogId = 1, Url = "http://sample.com" });
            builder.Entity<Post>().HasData(new Post { BlogId = 1, PostId = 1, Title = "First post", Content = "Test 1" });
            builder.Entity<Post>().HasData(new Post { BlogId = 1, PostId = 2, Title = "Second post", Content = "Test 2" });

            builder.Entity<Tag>().HasData(new Tag { TagId = 1, Name = "a" });
            builder.Entity<Tag>().HasData(new Tag { TagId = 2, Name = "b" });
            builder.Entity<Tag>().HasData(new Tag { TagId = 3, Name = "a" });
            builder.Entity<Tag>().HasData(new Tag { TagId = 4, Name = "c" });
            builder.Entity<Tag>().HasData(new Tag { TagId = 5, Name = "d" });

            builder.Entity<PostTag>().HasData(new PostTag[]
            {
                new PostTag(){PostId = 1, TagId = 3},
                new PostTag(){PostId = 1, TagId = 2},
                new PostTag(){PostId = 1, TagId = 5},
                new PostTag(){PostId = 2, TagId = 1},
                new PostTag(){PostId = 2, TagId = 2},
                new PostTag(){PostId = 2, TagId = 3},
                new PostTag(){PostId = 2, TagId = 4},
                new PostTag(){PostId = 1, TagId = 4},
            });

            base.OnModelCreating(builder);
        }
    }
}
