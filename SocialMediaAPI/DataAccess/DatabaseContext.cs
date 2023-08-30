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

        //sql server side programiranje ovde
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasMany<Post>().WithOne();
            modelBuilder.Entity<User>().HasMany<Comment>().WithOne();
            modelBuilder.Entity<Post>().HasMany<Comment>().WithOne();
            modelBuilder.Entity<Post>()
                .Property(p => p.Tags)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            modelBuilder.Entity<Post>()
                .Property(p => p.Likes)
                .HasConversion(
                    v => string.Join(',', v.ToString()),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList().Select(int.Parse).ToList()
                );
            modelBuilder.Entity<User>()
                .Property(p => p.FollowedUsersIds)
                .HasConversion(
                    v => string.Join(',', v.ToString()),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList().Select(int.Parse).ToList()
                );

        }
    }
}
