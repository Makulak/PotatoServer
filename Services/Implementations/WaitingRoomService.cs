using MongoDB.Driver;
using PotatoServer.Database.MongoDb;
using PotatoServer.Database.MongoDb.Settings;
using PotatoServer.Exceptions;
using PotatoServer.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PotatoServer.Services.Implementations
{
    public class WaitingRoomService : IWaitingRoomService
    {
        private readonly IMongoCollection<Room> _rooms;

        public WaitingRoomService(IMongoDbSettings mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.DatabaseName);

            _rooms = database.GetCollection<Room>(mongoDbSettings.RoomsCollectionName);
        }

        public List<Room> GetRooms()
        {
            return _rooms.Find(room => true).ToList();
        }

        public Room GetRoom(string id)
        {
            return _rooms.Find(room => room.Id == id).SingleOrDefault();
        }

        public Room CreateRoom(string name, string password)
        {
            var room = new Room
            {
                Name = name,
                Password = password,
                MaxPlayersCount = 4,
                Status = "Open",
            };
            _rooms.InsertOne(room);

            return room;
        }

        public string RemoveRoom(string id)
        {
            var room = _rooms.Find(room => room.Id == id).SingleOrDefault();

            if (room == null)
                throw new NotFoundException(); // TODO: Message

            _rooms.DeleteOne(room => room.Id == id);

            return id;
        }

        public void EnterRoom(string roomId, string username)
        {
            var room = _rooms.Find(room => room.Id == roomId).SingleOrDefault();

            if (room == null)
                throw new NotFoundException(); // TODO: Message

            if (room.Players == null)
                room.Players = new List<GamePlayer>();

            var player = room.Players.SingleOrDefault(gamePlayer => gamePlayer.Username == username);

            if (player != null)
            {
                room.Players.Remove(player);
                player.IsActive = true;
                room.Players.Add(player);
            }
            else
                room.Players.Add(new GamePlayer(username));

            _rooms.ReplaceOne(room => room.Id == roomId, room);
        }

        public void LeaveRoom(string roomId, string username)
        {
            var room = _rooms.Find(room => room.Id == roomId).SingleOrDefault();

            if (room == null)
                throw new NotFoundException(); // TODO: Message
            if (room.Players == null)
                throw new ServerErrorException(); //TODO: Message

            var player = room.Players.SingleOrDefault(gamePlayer => gamePlayer.Username == username);

            if (player != null)
            {
                room.Players.Remove(player);
                player.IsActive = false;
                room.Players.Add(player);
            }
            else
                throw new BadRequestException(); // TODO: Message
        }
    }
}
