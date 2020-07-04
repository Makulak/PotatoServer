using Microsoft.AspNetCore.SignalR;
using PotatoServer.Services.Interfaces;
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

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _connectionService.RemovePlayer(Context.User.Identity.Name);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
