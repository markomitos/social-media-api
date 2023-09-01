using SocialMediaAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaAPI.DataAccess
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Tag> Tags { get; set; }

        //sql server side programiranje ovde
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tag>().HasIndex(t => t.Content).IsUnique();
            modelBuilder.Entity<User>().HasMany<Post>().WithOne().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasMany<Comment>().WithOne().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Post>().HasMany<Comment>().WithOne().OnDelete(DeleteBehavior.Restrict);
            

            
            
        }
    }
}
