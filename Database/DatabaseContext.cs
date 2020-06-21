using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PotatoServer.Database.Models.Camasutra;
using PotatoServer.Database.Models.CleverWord;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using PotatoServer.Database.Models.Core;

namespace PotatoServer.Database
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<LoggedAction> Logs { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new PositionConfiguration());
            builder.ApplyConfiguration(new WordConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new LoggedActionConfiguration());

            base.OnModelCreating(builder);
        }

        #region SaveChangesOverrides

        public override int SaveChanges()
        {
            SetModifiedDateTime();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetModifiedDateTime();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetModifiedDateTime();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            SetModifiedDateTime();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion

        private void SetModifiedDateTime()
        {
            var modified = ChangeTracker.Entries<IBaseModel>()
                            .Where(e => e.State == EntityState.Modified)
                            .ToList();

            modified.ForEach(e =>
            {
                e.Property(x => x.Changed).CurrentValue = DateTime.Now;
                e.Property(x => x.Changed).IsModified = true;

                e.Property(x => x.Created).IsModified = false;
            });
        }
    }
}
