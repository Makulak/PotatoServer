using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using PotatoServer.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PotatoServer.Database
{
    public abstract class CoreDatabaseContext<TUser> : IdentityDbContext<TUser> where TUser : IdentityUser, new()
    {
        public CoreDatabaseContext() { }
        public CoreDatabaseContext(DbContextOptions<CoreDatabaseContext<TUser>> options) : base(options) { }
        protected CoreDatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });

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
                            .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
                            .ToList();

            modified.ForEach(e =>
            {
                if (e.State == EntityState.Modified)
                {
                    e.Property(x => x.Changed).CurrentValue = DateTime.Now;
                    e.Property(x => x.Changed).IsModified = true;
                }
                else if(e.State == EntityState.Added)
                {
                    e.Property(x => x.Created).CurrentValue = DateTime.Now;
                    e.Property(x => x.Created).IsModified = true;
                }
            });
        }
    }
}
