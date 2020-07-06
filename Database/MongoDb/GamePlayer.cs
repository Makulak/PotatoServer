namespace PotatoServer.Database.MongoDb
{
    public class GamePlayer
    {
        public string Username { get; }
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
