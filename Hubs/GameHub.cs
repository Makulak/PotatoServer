using Microsoft.AspNetCore.SignalR;
using PotatoServer.Exceptions;
using System.Threading.Tasks;

namespace PotatoServer.Hubs
{
    public partial class PotatoHub : Hub
    {
        public async Task TryEnterGame(int roomId, string password)
        {
            var room = _waitingRoomService.GetRoom(roomId);
            if (room == null)
                throw new NotFoundException(); // TODO: Message

            if (room.HasPassword && password != room.Password)
                throw new BadRequestException(); //TODO: Message

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "WaitingRoom");
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            await Clients.Group("WaitingRoom").SendAsync("updateRoom", );
        }

        public async Task LeaveGame(int rooomId)
        {
            await Clients.Group("WaitingRoom").SendAsync("updateRoom", );
        }

        public async Task UpdateGameSettings()
        {

        }
    }
}
