using Microsoft.AspNetCore.SignalR;
using PotatoServer.Exceptions;
using System.Threading.Tasks;

namespace PotatoServer.Hubs
{
    public partial class PotatoHub : Hub
    {
        public async Task TryEnterGame(int id, string password)
        {
            var room = _waitingRoomService.GetRoom(id);
            if (room == null)
                throw new NotFoundException(); // TODO: Message

            if (room.HasPassword)
            {
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());
        }
    }
}
