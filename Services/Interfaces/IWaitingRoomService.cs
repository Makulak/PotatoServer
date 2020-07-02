using PotatoServer.Models;
using System.Collections.Generic;

namespace PotatoServer.Services.Interfaces
{
    public interface IWaitingRoomService
    {
        public List<Room> GetRooms();
        public Room GetRoom(int id);
        public Room CreateRoom(string name, string password);
        public int RemoveRoom(int id);
    }
}
