using Microsoft.AspNetCore.SignalR;
using PotatoServer.Exceptions;
using System.Threading.Tasks;

namespace PotatoServer.Hubs
{
    public partial class PotatoHub : Hub
    {
        public async Task TryEnterGame(string roomId, string password)
        {
            var room = _waitingRoomService.GetRoom(roomId);

            if (room == null)
                throw new NotFoundException(); // TODO: Message

            if (room.HasPassword && password != room.Password)
                throw new BadRequestException(); //TODO: Message

            if (room.MaxPlayersCount == room.PlayersCount)
                throw new BadRequestException(); //TODO: Message

            _waitingRoomService.EnterRoom(roomId, UserIdentityName);

            var roomVm = _mapper.MapToRoomViewModel(room);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "WaitingRoom");
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            await Clients.Group("WaitingRoom").SendAsync("updateRoom", roomVm);
            await Clients.Group(roomId).SendAsync("addToPlayersList", new { username = UserIdentityName, Score = 0 });
        }

        public async Task LeaveGame(string roomId)
        {
            var room = _waitingRoomService.GetRoom(roomId);

            if (room == null)
                throw new NotFoundException(); // TODO: Message

            var roomVm = _mapper.MapToRoomViewModel(room);

            if (roomVm.PlayersCount == 1)
                _waitingRoomService.RemoveRoom(roomId);
            else
                _waitingRoomService.LeaveRoom(roomId, UserIdentityName);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);

            await Clients.Group(roomId).SendAsync("removePlayerFromList", new { username = UserIdentityName });
            await Clients.Group("WaitingRoom").SendAsync("updateRoom", roomVm);
        }
    }
}
