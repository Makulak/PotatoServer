using PotatoServer.Exceptions;
using PotatoServer.Models;
using PotatoServer.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PotatoServer.Services.Implementations
{
    public class ConnectionService : IConnectionService
    {
        private readonly List<UserConnection> players;

        public ConnectionService()
        {
            players = new List<UserConnection>();
        }

        public void RemovePlayer(string userName)
        {
            var player = players.SingleOrDefault(player => player.Username == userName);

            if (player == null)
                throw new NotFoundException(); //TODO: message

            players.Remove(player);
        }

        public void AddPlayer(string userName, string connectionId)
        {
            var player = players.SingleOrDefault(player => player.Username == userName);

            if (player != null)
                throw new ServerErrorException(); //TODO: message

            players.Add(new UserConnection
            {
                Username = userName,
                ConnectionId = connectionId
            });
        }

        public UserConnection GetPlayer(string userName)
        {
            return players.SingleOrDefault(player => player.Username == userName);
        }

        public void UpdateRoomId(string userName, string roomId)
        {
            var player = players.SingleOrDefault(player => player.Username == userName);

            if (player != null)
                player.RoomId = roomId;
        }
    }
}
