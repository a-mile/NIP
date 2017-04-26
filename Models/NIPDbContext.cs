using Microsoft.EntityFrameworkCore;

namespace NIP.Models
{
    public class NIPDbContext : DbContext
    {
        public NIPDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<Log> Logs { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}