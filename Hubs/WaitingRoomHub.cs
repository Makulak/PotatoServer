using Microsoft.AspNetCore.SignalR;
using PotatoServer.Exceptions;
using PotatoServer.ViewModels;
using System.Threading.Tasks;

namespace PotatoServer.Hubs
{
    public partial class PotatoHub : Hub
    {
        public async Task EnterWaitingRoom()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "WaitingRoom");
        }

        public async Task GetRooms()
        {
            var rooms = _waitingRoomService.GetRooms();
            var roomsVm = _mapper.MapToRoomViewModel(rooms);

            await Clients.Caller.SendAsync("updateRoomsList", roomsVm);
        }

        public async Task CreateRoom(RoomViewModel_Post room)
        {
            var createdRoom = _waitingRoomService.CreateRoom(room.Name, room.Password);

            if (createdRoom != null)
            {
                var createdRoomVm = _mapper.MapToRoomViewModel(createdRoom);

                await Groups.AddToGroupAsync(Context.ConnectionId, createdRoomVm.Id);
                await Clients.Caller.SendAsync("navigateToRoom", new { roomId = createdRoomVm.Id});

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "WaitingRoom");
                await Clients.Group("WaitingRoom").SendAsync("addRoomToList", createdRoomVm);
            }
            else
                throw new ServerErrorException(); //TODO: Message
        }

        public async Task RemoveRoom(string id)
        {
            var deletedId = _waitingRoomService.RemoveRoom(id);

            if (!string.IsNullOrEmpty(deletedId))
                await Clients.Group("WaitingRoom").SendAsync("removeRoomFromList", new { roomId = id });
            else
                throw new ServerErrorException(); //TODO: Message
        }
    }
}
