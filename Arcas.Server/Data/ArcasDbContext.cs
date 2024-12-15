using Microsoft.EntityFrameworkCore;
using sqlTest.Server.Models;

namespace sqlTest.Server.Data
{
    public class ArcasDbContext : DbContext
    {
        public DbSet<Article> Articals {get; set;}
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public ArcasDbContext(DbContextOptions<ArcasDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
              .HasKey(b => b.Id);

            // 配置 Book 与 User 的关系（假设一对多）没改好
            //modelBuilder.Entity<Book>()
            //    .HasOne(b => b.OwnerId)
            //    .WithMany(u => u.books)
            //    .HasForeignKey(b => b.OwnerId)
            //    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
