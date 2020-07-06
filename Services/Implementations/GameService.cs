using MongoDB.Driver;
using PotatoServer.Database.MongoDb;
using PotatoServer.Database.MongoDb.Settings;
using PotatoServer.Services.Interfaces;

namespace PotatoServer.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IMongoCollection<Game> _games;

        public GameService(IMongoDbSettings mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.DatabaseName);

            _games = database.GetCollection<Game>(mongoDbSettings.GamesCollectionName);
        }
    }
}
