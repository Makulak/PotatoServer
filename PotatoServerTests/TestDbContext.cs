using Microsoft.EntityFrameworkCore;
using PotatoServer.Database;
using PotatoServer.Database.Models;

namespace PotatoServerTestsCore
{
    // Only for test purposes
    internal class TestDbContext : BaseDbContext<User>
    {
        public TestDbContext(DbContextOptions options) : base(options) { }
    }
}
