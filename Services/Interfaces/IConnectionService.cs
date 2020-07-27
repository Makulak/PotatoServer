using PotatoServer.Models;

namespace PotatoServer.Services.Interfaces
{
    public interface IConnectionService
    {
        public void RemovePlayer(string userName);
        public void AddPlayer(string userName, string connectionId);
        public UserConnection GetPlayer(string userName);
        public void UpdateRoomId(string userName, string roomId);
    }
}
