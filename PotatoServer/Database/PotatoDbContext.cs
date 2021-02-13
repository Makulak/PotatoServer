using Microsoft.EntityFrameworkCore;
using PotatoServer.Database.Models;

namespace PotatoServer.Database
{
    public class PotatoDbContext : BaseDbContext<PotatoUser>
    {
        public PotatoDbContext(DbContextOptions options) : base(options) { }
    }
}
