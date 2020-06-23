using Microsoft.AspNetCore.SignalR;
using PotatoServer.Services;
using System;
using System.Threading.Tasks;

namespace PotatoServer.Hubs.WaitingRoom
{
    public class WaitingRoomHub : Hub
    {
        private readonly WaitingRoomService _service;

        public WaitingRoomHub(WaitingRoomService service)
        {
            _service = service;
        }
        public override async Task OnConnectedAsync()
        {
            _service.AddPlayer(Context.User.Identity.Name, Context.ConnectionId);
            await Clients.All.SendAsync("updatePlayerCount", new { count = this._service.GetPlayersCount() });
            await Clients.All.SendAsync("updateAllRooms", _service.GetRooms());
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _service.RemovePlayer(Context.User.Identity.Name);
            await Clients.All.SendAsync("updatePlayerCount", new { count = this._service.GetPlayersCount() });
            await base.OnDisconnectedAsync(exception);
        }

        public async Task GetAllRooms()
        {
            await Clients.All.SendAsync("updateAllRooms", _service.GetRooms());
        }

        public async Task AddRoom(string roomName)
        {
            var room = _service.AddRoom(roomName);

            if(room != null)
                await Clients.All.SendAsync("roomAdded", room);
        }

        public async Task RemoveRoom(string roomName)
        {
            var id = _service.RemoveRoom(roomName);

            if(id != 0)
                await Clients.All.SendAsync("roomRemoved", new { roomId = id });
        }
    }
}
