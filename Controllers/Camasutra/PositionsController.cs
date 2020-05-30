using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PotatoServer.Database;
using System;
using Microsoft.AspNetCore.Authorization;
using PotatoServer.ViewModels.Camasutra.Position;
using PotatoServer.Services.Mapping;
using PotatoServer.Exceptions;
using Microsoft.Extensions.Localization;
using System.Linq;
using PotatoServer.Helpers;
using Microsoft.AspNetCore.Cors;

namespace PotatoServer.Controllers.Camasutra
{
    [Route("api/camasutra/categories/{categoryId}/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly PositionMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public PositionsController(DatabaseContext context,
            PositionMapper mapper,
            IStringLocalizer<SharedResources> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionGetVm>>> GetPositions(int categoryId, [Minimum(0)] int? skip, [Minimum(0)] int? take)
        {

            if (!(await _context.Positions.AnyAsync(category => category.CategoryId == categoryId)))
                throw new NotFoundException(_localizer.GetString("NotFound_Category"));

            var positions = await _context.Positions
                .Where(position => position.CategoryId == categoryId)
                .GetPaged(skip, take)
                .ToListAsync();

            return Ok(_mapper.MapToPositionGetVm(positions));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionGetVm>> GetPosition(int categoryId, int positionId)
        {
            try
            {
                var position = await _context.Positions
                .FirstOrDefaultAsync(position => position.CategoryId == categoryId && position.Id == positionId);

                if (position == null)
                    throw new NotFoundException(_localizer.GetString("NotFound_Position", positionId));

                return Ok(_mapper.MapToPositionGetVm(position));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PositionGetVm>> PostPosition(int categoryId, PositionPostVm positionVm)
        {
            var position = _mapper.MapToPosition(positionVm);
            position.CategoryId = categoryId;

            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosition", new { categoryId, id = position.Id }, _mapper.MapToPositionGetVm(position));

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<PositionGetVm>> DeletePosition(int categoryId, int positionId)
        {
            var position = await _context.Positions
            .FirstOrDefaultAsync(position => position.CategoryId == categoryId && position.Id == positionId);

            if (position == null)
                throw new NotFoundException(_localizer.GetString("NotFound_Position", positionId));

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return Ok(_mapper.MapToPositionGetVm(position));

        }
    }
}
