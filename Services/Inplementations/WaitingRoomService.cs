using PotatoServer.Exceptions;
using PotatoServer.Models;
using PotatoServer.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PotatoServer.Services
{
    public class WaitingRoomService : IWaitingRoomService
    {
        private List<Room> rooms;
        private int lastRoomId;

        public WaitingRoomService()
        {
            rooms = new List<Room>();
        }

        public List<Room> GetRooms()
        {
            return rooms;
        }

        public Room GetRoom(int id)
        {
            return rooms.SingleOrDefault(room => room.Id == id);
        }

        public Room CreateRoom(string name, string password)
        {
            var room = new Room
            {
                Id = ++lastRoomId,
                Name = name,
                Password = password,
                MaxPlayersCount = 2,
                PlayersCount = 0,
                Status = "Open"
            };
            rooms.Add(room);

            return room;
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
