using BlogProject.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccessLayer.Concrete
{
    public class Context:IdentityDbContext<IdentityUser<int>,IdentityRole<int>,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-97JKMRT\\SQLEXPRESS;Database=BlogProject-asp5;integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message2>()
                .HasOne(x => x.SenderUser)
                .WithMany(y => y.WriterSender)
                .HasForeignKey(z => z.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Message2>()
                .HasOne(x => x.ReceiverUser)
                .WithMany(y => y.WriterReceiver)
                .HasForeignKey(z => z.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" }
            );

            var hasher = new PasswordHasher<IdentityUser<int>>();
            var adminUser = new IdentityUser<int>
            {
                Id = 1,
                UserName = "admin@blog.com",
                NormalizedUserName = "ADMIN@BLOG.COM",
                Email = "admin@blog.com",
                NormalizedEmail = "ADMIN@BLOG.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin123");

            modelBuilder.Entity<IdentityUser<int>>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 }
            );
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<BlogRayting> BlogRaytings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Message2> Messages2 { get; set; }

    }
}
