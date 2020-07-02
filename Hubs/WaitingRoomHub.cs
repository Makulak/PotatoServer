using Microsoft.AspNetCore.SignalR;
using PotatoServer.Exceptions;
using PotatoServer.ViewModels;
using System.Threading.Tasks;

namespace PotatoServer.Hubs
{
    public partial class PotatoHub : Hub
    {
        public async Task GetRooms()
        {
            var rooms = _waitingRoomService.GetRooms();
            await Clients.Caller.SendAsync("updateRoomsList", rooms);
        }

        public async Task CreateRoom(RoomViewModel_Post room)
        {
            var createdRoom = _waitingRoomService.CreateRoom(room.Name, room.Password);

            if (createdRoom != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, createdRoom.Id.ToString());
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "WaitingRoom");

                await Clients.Group("WaitingRoom").SendAsync("addRoomToList", createdRoom);
            }
            else
                throw new ServerErrorException(); //TODO: Message
        }

        public async Task RemoveRoom(int id)
        {
            var deletedId = _waitingRoomService.RemoveRoom(id);

            if (deletedId != 0)
                await Clients.Group("WaitingRoom").SendAsync("removeRoomFromList", new { roomId = id });
            else
                throw new ServerErrorException(); //TODO: Message
        }
    }
}
