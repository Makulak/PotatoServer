using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace PotatoServer.Hubs.Rooms
{
    [Authorize]
    public class RoomsHub : Hub
    {
        private readonly IRoomRepository _roomRepository;

        public RoomsHub(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public override async Task OnConnectedAsync()
        {
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("getRooms")]
        public async Task GetRooms(int skip, int take)
        {
            var rooms = _roomRepository.GetRooms(skip, take);
            await Clients.All.SendAsync("onGetRooms", rooms);
        }
    }
}
