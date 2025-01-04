using Microsoft.EntityFrameworkCore;
using Arcas.Server.Models;

namespace Arcas.Server.Data
{
    public class ArcasDbContext : DbContext
    {
        public DbSet<Comment> Comment { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ExchangeDetail> ExchangeDetail { get; set; }
        public DbSet<AbstractBook> AbstractBook { get; set; }
        public DbSet<ConcreteBook> ConcreteBook { get; set; }

        public ArcasDbContext(DbContextOptions<ArcasDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
