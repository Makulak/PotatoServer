using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PotatoServer.Database.MongoDb
{
    public class GamePlayer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }

        public GamePlayer(string username)
        {
            Username = username;
            Points = 0;
            IsActive = true;
        }
    }
}
