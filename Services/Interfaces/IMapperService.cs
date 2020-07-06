using PotatoServer.Database.MongoDb;
using PotatoServer.ViewModels;
using System.Collections.Generic;

namespace PotatoServer.Services.Interfaces
{
    public interface IMapperService
    {
        RoomViewModel_Get MapToRoomViewModel(Room room);
        IEnumerable<RoomViewModel_Get> MapToRoomViewModel(IEnumerable<Room> rooms);
    }
}
