namespace PotatoServer.ViewModels
{
    public class RoomViewModel_Get
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int PlayersCount { get; set; }
        public int MaxPlayersCount { get; set; }
        public bool HasPassword { get; set; }
    }
}
