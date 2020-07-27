using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using PotatoServer.Exceptions;
using PotatoServer.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PotatoServer.Hubs
{
    public partial class PotatoHub : Hub
    {
        private readonly IWaitingRoomService _waitingRoomService;
        private readonly IConnectionService _connectionService;
        private readonly IGameService _gameService;
        private readonly IMapperService _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public PotatoHub(IWaitingRoomService waitingRoomService,
                         IConnectionService connectionService,
                         IGameService gameService,
                         IMapperService mapper,
                         IStringLocalizer<SharedResources> localizer)
        {
            _waitingRoomService = waitingRoomService;
            _connectionService = connectionService;
            _gameService = gameService;
            _mapper = mapper;
            _localizer = localizer;
        }

        public override async Task OnConnectedAsync()
        {
            _connectionService.AddPlayer(UserIdentityName, Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _connectionService.GetPlayer(UserIdentityName);

            if (user == null)
                throw new ServerErrorException(); //TODO: Message

            _connectionService.RemovePlayer(UserIdentityName);

            if (!string.IsNullOrEmpty(user.RoomId))
                await LeaveRoom(user.RoomId); //TODO: Is it ok?

            await base.OnDisconnectedAsync(exception);
        }

        private string UserIdentityName => Context.User.Identity.Name;
    }
}
