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

        public async Task TryEnterRoom(string roomId, string password)
        {
            // Getting room to check requirements
            var room = _waitingRoomService.GetRoom(roomId);

            if (room == null)
                throw new NotFoundException(); // TODO: Message

            if (room.HasPassword && password != room.Password)
                throw new BadRequestException(); //TODO: Message

            if (room.MaxPlayersCount == room.PlayersCount)
                throw new BadRequestException(); //TODO: Message

            _waitingRoomService.EnterRoom(roomId, UserIdentityName);

            // Getting room to send updated object
            room = _waitingRoomService.GetRoom(roomId);

            var roomVm = _mapper.MapToRoomViewModel(room);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "WaitingRoom");
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            await Clients.Group("WaitingRoom").SendAsync("updateRoomOnList", roomVm);
            await Clients.Group(roomId).SendAsync("addToPlayersList", new { username = UserIdentityName, Score = 0 });
        }

        public async Task LeaveRoom(string roomId)
        {
            var room = _waitingRoomService.GetRoom(roomId);

            if (room == null)
                throw new NotFoundException(); // TODO: Message

            var roomVm = _mapper.MapToRoomViewModel(room);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            
            if (roomVm.PlayersCount == 1)
            {
                _waitingRoomService.RemoveRoom(roomId);
                await Clients.Group("WaitingRoom").SendAsync("removeRoomFromList", new { roomId = roomId });
            }
            else
            {
                _waitingRoomService.LeaveRoom(roomId, UserIdentityName);
                await Clients.Group(roomId).SendAsync("removePlayerFromList", new { username = UserIdentityName });
                await Clients.Group("WaitingRoom").SendAsync("updateRoomOnList", roomVm);
            }
        }
    }
}
