using Microsoft.EntityFrameworkCore;
using sqlTest.Server.Models;

namespace sqlTest.Server.Data
{
    public class ArcasDbContext : DbContext
    {
        public DbSet<Artical> Articals {get; set;}
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public ArcasDbContext(DbContextOptions<ArcasDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
