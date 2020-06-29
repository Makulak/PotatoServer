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
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _service.RemovePlayer(Context.User.Identity.Name);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task GetAllRooms()
        {
            await Clients.All.SendAsync("updateAllRooms", _service.GetRooms());
        }

        public async Task CreateRoom(string roomName, string password)
        {
            var room = _service.CreateRoom(roomName, password);

            if(room != null)
                await Clients.All.SendAsync("createRoom", room);
        }

        public async Task RemoveRoom(string roomName)
        {
            var id = _service.RemoveRoom(roomName);

            if(id != 0)
                await Clients.All.SendAsync("removeRoom", new { roomId = id });
        }

        public async Task EnterToRoom(int roomId)
        {
            var room = _service.GetRoom(roomId);
            if (room == null)
                return;

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }
    }
}
