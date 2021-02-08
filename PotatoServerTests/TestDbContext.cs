using Microsoft.EntityFrameworkCore;
using PotatoServer.Database;
using PotatoServer.Database.Models;

namespace PotatoServerTests
{
    // Only for test purposes
    internal class TestDbContext : CoreDatabaseContext<User>
    {
        public TestDbContext(DbContextOptions options) : base(options) { }
    }
}
