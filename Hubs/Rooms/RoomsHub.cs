using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotatoServer.Hubs.Rooms
{
    public class RoomsHub : Hub
    {
        public async Task GetRooms(int? skip, int? take)
        {
            await Task.FromResult("");
        }
    }
}
