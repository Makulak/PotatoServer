using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace PotatoServer.Hubs.Rooms
{
    public class RoomsHub : Hub
    {
        private readonly IRoomRepository _roomRepository;

        public RoomsHub(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task GetRooms(int skip, int take)
        {
            var rooms = _roomRepository.GetRooms(skip, take);
            await Clients.All.SendAsync("onGetRooms", rooms);
        }

        public async Task PlayerEntered()
        {
           await Clients.All.SendAsync("OnPlayerEntered");
        }
    }
}
