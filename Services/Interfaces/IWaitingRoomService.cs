using PotatoServer.ViewModels;
using System.Collections.Generic;

namespace PotatoServer.Services.Interfaces
{
    public interface IWaitingRoomService
    {
        public List<RoomViewModel_Get> GetRooms();
        public RoomViewModel_Get GetRoom(int id);
        public RoomViewModel_Get CreateRoom(string name, string password);
        public int RemoveRoom(int id);
    }
}
