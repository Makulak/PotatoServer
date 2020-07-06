using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace PotatoServer.Database.MongoDb
{
    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public List<GamePlayer> Players { get; set; }
        [BsonIgnore]
        public int PlayersCount => Players?.Count ?? 0;
        public int MaxPlayersCount { get; set; }
        [BsonIgnore]
        public bool HasPassword => !string.IsNullOrEmpty(Password);
        public string Password { get; set; }
        public Game Game { get; set; }
    }
}
