using System.Collections.Generic;

namespace PotatoServer.Database.MongoDb
{
    public class Game
    {
        public List<int> CardSetIds { get; set; }
        public string KingPlayerUsername { get; set; }
        public int RoundsPlayed { get; set; }
        public bool RoundStarted { get; set; }
        public bool GameStarted { get; set; }
        public GameSettings Settings { get; set; }
    }
}
