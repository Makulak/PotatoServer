using System.Collections.Generic;

namespace PotatoServer.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public List<string> Players { get; set; }
        public int MaxPlayersCount { get; set; }
        public bool HasPassword { get; set; }
        public string Password { get; set; }
    }
}
