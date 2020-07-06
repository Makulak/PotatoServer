using PotatoServer.Database.MongoDb;
using System.Collections.Generic;

namespace PotatoServer.Services.Interfaces
{
    public interface IWaitingRoomService
    {
        public List<Room> GetRooms();
        public Room GetRoom(string id);
        public Room CreateRoom(string name, string password);
        public string RemoveRoom(string id);
        public void EnterRoom(string roomId, string username);
        public void LeaveRoom(string roomId, string username);
    }
}
