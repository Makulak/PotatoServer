using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotatoServer.Hubs.Rooms
{
    public class RoomsRepository : IRoomRepository
    {
        private List<RoomGetVm> rooms;

        public RoomsRepository()
        {
            rooms = new List<RoomGetVm>();
        }

        public List<RoomGetVm> GetRooms(int skip, int take)
        {
            return rooms.Skip(skip).Take(take).ToList();
        }
    }
}
