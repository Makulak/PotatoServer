using Microsoft.AspNetCore.SignalR;
using PotatoServer.Hubs.WaitingRoom;
using System.Threading.Tasks;

namespace PotatoServer.Hubs
{
    public class GameHub : Hub
    {
        private readonly IHubContext<WaitingRoomHub> _waitingRoomHubContext;

        public GameHub(IHubContext<WaitingRoomHub> waitingRoomHubContext)
        {
            _waitingRoomHubContext = waitingRoomHubContext;
        }

        public async Task Test(int roomId)
        {
            await Clients.Group(roomId.ToString()).SendAsync("test");
        }
    }
}
