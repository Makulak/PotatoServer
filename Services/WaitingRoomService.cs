using PotatoServer.Models;
using System.Collections.Generic;
using System.Linq;

namespace PotatoServer.Services
{
    public class WaitingRoomService
    {
        private Dictionary<string, string> players;
        private List<Room> rooms;
        private int lastRoomId;

        public WaitingRoomService()
        {
            rooms = new List<Room>();
            players = new Dictionary<string, string>();
        }

        public List<Room> GetRooms()
        {
            return rooms;
        }

        public Room GetRoom(int id)
        {
            return rooms.SingleOrDefault(room => room.Id == id);
        }

        public void RemovePlayer(string userName)
        {
            players.Remove(userName);
        }

        public void AddPlayer(string userName, string connectionId)
        {
            var player = players.SingleOrDefault(player => player.Key == userName);
            if (!player.Equals(default(KeyValuePair<string,string>)))
            {
                players[userName] = connectionId;
            }
            else
            {
                players.Add(userName, connectionId);
            }
        }

        public Room CreateRoom(string name, string password)
        {
            if (rooms.Any(room => room.Name == name))
                return null;

            var room = new Room
            {
                Id = ++lastRoomId,
                Name = name,
                HasPassword = !string.IsNullOrEmpty(password),
                Password = password,
                MaxPlayersCount = 4,
                PlayersCount = 0,
                Status = "Open"
            };
            rooms.Add(room);

            return room;
        }

        public int RemoveRoom(string name)
        {
            var room = rooms.SingleOrDefault(room => room.Name == name);

            if (room == null)
                return 0;

            rooms.Remove(room);

            return room.Id;
        }
    }
}
