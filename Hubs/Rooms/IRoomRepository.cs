using System.Collections.Generic;

namespace PotatoServer.Hubs.Rooms
{
    public interface IRoomRepository
    {
        public List<RoomGetVm> GetRooms(int skip, int take);
    }
}
