using PotatoServer.Exceptions;
using PotatoServer.Models;
using PotatoServer.Services.Interfaces;
using PotatoServer.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PotatoServer.Services
{
    public class WaitingRoomService : IWaitingRoomService
    {
        private List<Room> rooms;
        private int lastRoomId;
        private readonly MapperService _mapper;

        public WaitingRoomService(MapperService mapper)
        {
            rooms = new List<Room>();
            _mapper = mapper;
        }

        public List<RoomViewModel_Get> GetRooms()
        {
            return _mapper.MapToRoomViewModel(rooms).ToList();
        }

        public RoomViewModel_Get GetRoom(int id)
        {
            var room = rooms.SingleOrDefault(room => room.Id == id);
            return _mapper.MapToRoomViewModel(room);
        }

        public RoomViewModel_Get CreateRoom(string name, string password)
        {
            var room = new Room
            {
                Id = ++lastRoomId,
                Name = name,
                Password = password,
                MaxPlayersCount = 4,
                PlayersCount = 0,
                Status = "Open"
            };
            rooms.Add(room);

            return _mapper.MapToRoomViewModel(room);
        }

        public int RemoveRoom(int id)
        {
            var room = rooms.SingleOrDefault(room => room.Id == id);

            if (room == null)
                throw new NotFoundException(); // TODO: Message

            rooms.Remove(room);

            return id;
        }
    }
}
