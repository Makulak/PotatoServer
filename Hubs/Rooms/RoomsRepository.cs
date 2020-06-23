using System.Collections.Generic;
using System.Linq;

namespace PotatoServer.Hubs.Rooms
{
    public class RoomsRepository : IRoomRepository
    {
        private List<RoomGetVm> rooms;
        private List<string> users;

        public RoomsRepository()
        {
            rooms = new List<RoomGetVm>();
            users = new List<string>();
        }

        public List<RoomGetVm> GetRooms(int skip, int take)
        {
            return rooms.Skip(skip).Take(take).ToList();
        }

        public int GetPlayerCount()
        {
            return users.Count;
        }

        public void AddPlayer(string userName)
        {
            users.Add(userName);
        }
    }
}
