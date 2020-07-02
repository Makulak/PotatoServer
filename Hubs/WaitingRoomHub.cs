using Microsoft.AspNetCore.SignalR;
using PotatoServer.Services.Interfaces;
using PotatoServer.ViewModels;
using System;
using System.Threading.Tasks;

namespace PotatoServer.Hubs
{
    public partial class PotatoHub : Hub
    {
        private readonly IWaitingRoomService _waitingRoomService;
        private readonly IConnectionService _connectionService;

        public PotatoHub(IWaitingRoomService waitingRoomService,
                              IConnectionService connectionService)
        {
            _waitingRoomService = waitingRoomService;
            _connectionService = connectionService;
        }

        public override async Task OnConnectedAsync()
        {
            _connectionService.AddPlayer(Context.User.Identity.Name, Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, "WaitingRoom");
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
            var rooms = _waitingRoomService.GetRooms();
            await Clients.Group("WaitingRoom").SendAsync("updateAllRooms", rooms);
        }

        public async Task CreateRoom(RoomViewModel_Post room)
        {
            var createdRoom = _waitingRoomService.CreateRoom(room.Name, room.Password);

            if (createdRoom != null)
                await Clients.Group("WaitingRoom").SendAsync("createRoom", createdRoom);
        }

        public async Task RemoveRoom(int id)
        {
            var deletedId = _waitingRoomService.RemoveRoom(id);

            if (deletedId != 0)
                await Clients.Group("WaitingRoom").SendAsync("removeRoom", new { roomId = id });
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
