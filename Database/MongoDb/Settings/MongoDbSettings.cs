namespace PotatoServer.Database.MongoDb.Settings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string GamesCollectionName { get; set; }
        public string RoomsCollectionName { get; set; }
        public string GamePlayersCollectionName { get; set; }
        public string GameSettingsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
