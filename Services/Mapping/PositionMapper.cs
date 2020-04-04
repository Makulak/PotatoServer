using PotatoServer.Database.Models.Camasutra;
using PotatoServer.ViewModels.Camasutra.Position;
using System.Collections.Generic;
using System.Linq;

namespace PotatoServer.Services.Mapping
{
    public class PositionMapper
    {
        public PositionMapper()
        {
        }

        public Position MapToPosition(PositionPostVm positionVm)
        {
            if (positionVm == null)
                return null;

            return new Position
            {
                Name = positionVm.Name,
                Description = positionVm.Description,
            };
        }

        public PositionGetVm MapToPositionGetVm(Position position)
        {
            if (position == null)
                return null;

            return new PositionGetVm
            {
                Id = position.Id,
                Name = position.Name,
                Description = position.Description
            };
        }

        public IEnumerable<PositionGetVm> MapToPositionGetVm(IEnumerable<Position> positions)
        {
            if (positions == null)
                return null;

            return positions.Select(position => MapToPositionGetVm(position));
        }
    }
}
