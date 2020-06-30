using Microsoft.AspNetCore.SignalR;
using PotatoServer.Services.Interfaces;
using PotatoServer.ViewModels;
using System;
using System.Threading.Tasks;

namespace PotatoServer.Hubs.WaitingRoom
{
    public class WaitingRoomHub : Hub
    {
        private readonly IWaitingRoomService _waitingRoomService;
        private readonly IConnectionService _connectionService;

        public WaitingRoomHub(IWaitingRoomService waitingRoomService,
                              IConnectionService connectionService)
        {
            _waitingRoomService = waitingRoomService;
            _connectionService = connectionService;
        }

        public override async Task OnConnectedAsync()
        {
            _connectionService.AddPlayer(Context.User.Identity.Name, Context.ConnectionId);
            await Clients.Caller.SendAsync("updateAllRooms", _waitingRoomService.GetRooms());
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _connectionService.RemovePlayer(Context.User.Identity.Name);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task GetAllRooms()
        {
            await Clients.All.SendAsync("updateAllRooms", _waitingRoomService.GetRooms());
        }

        public async Task CreateRoom(RoomViewModel_Post room)
        {
            var createdRoom = _waitingRoomService.CreateRoom(room.Name, room.Password);

            if (createdRoom != null)
                await Clients.All.SendAsync("createRoom", createdRoom);
        }

        public async Task RemoveRoom(int id)
        {
            _waitingRoomService.RemoveRoom(id);

            if (id != 0)
                await Clients.All.SendAsync("removeRoom", new { roomId = id });
        }

        public async Task TryEnterRoom(int id)
        {
            var room = _waitingRoomService.GetRoom(id);
            if (room == null)
                return;

            await Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());
        }
    }
}
