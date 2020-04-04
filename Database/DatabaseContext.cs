using PotatoServer.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PotatoServer.Database.Models.Camasutra;
using PotatoServer.Database.Models.CleverWord;

namespace PotatoServer.Database
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Word> Words { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Word>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<Position>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<Category>()
                .HasIndex(u => u.Name)
                .IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
