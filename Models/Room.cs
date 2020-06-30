﻿using System.Collections.Generic;

namespace PotatoServer.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int PlayersCount { get; set; }
        public int MaxPlayersCount { get; set; }
        public bool HasPassword => !string.IsNullOrEmpty(Password);
        public string Password { get; set; }
    }
}
