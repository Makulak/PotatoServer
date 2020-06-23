using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace PotatoServer.Database.Models.Core
{
    public class LoggedAction
    {
        public int Id { get; set; }
        [Required]
        public string ControllerName { get; set; }
        [Required]
        public string ActionName { get; set; }
        [Required]
        public string Method { get; set; }
        [Required]
        public string Path { get; set; }
        public User User { get; set; }

        public string UserId { get; set; }
    }

    public class LoggedActionConfiguration : IEntityTypeConfiguration<LoggedAction>
    {
        public void Configure(EntityTypeBuilder<LoggedAction> builder)
        {
        }
    }
}
