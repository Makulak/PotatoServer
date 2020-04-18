using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PotatoServer.Database.Models.Core
{
    public class LoggedAction
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }

    public class LoggedActionConfiguration : IEntityTypeConfiguration<LoggedAction>
    {
        public void Configure(EntityTypeBuilder<LoggedAction> builder)
        {
        }
    }
}
