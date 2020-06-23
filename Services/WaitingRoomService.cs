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

        public int GetPlayersCount()
        {
            return players.Count;
        }

        public Room AddRoom(string name)
        {
            if (rooms.Any(room => room.Name == name))
                return null;

            var room = new Room
            {
                Id = ++lastRoomId,
                Name = name
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
