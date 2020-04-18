using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PotatoServer.Database;
using System;
using Microsoft.AspNetCore.Authorization;
using PotatoServer.ViewModels.Camasutra.Category;
using PotatoServer.Services.Mapping;
using PotatoServer.Exceptions;
using Microsoft.Extensions.Localization;
using PotatoServer.Helpers;

namespace PotatoServer.Controllers.Camasutra
{
    [Route("api/camasutra/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly CategoryMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public CategoriesController(DatabaseContext context,
            CategoryMapper mapper,
            IStringLocalizer<SharedResources> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryGetVm>>> GetCategories([Minimum(0)] int? skip, [Minimum(0)] int? take)
        {
            try
            {
                var categories = await _context.Categories
                    .GetPaged(skip, take)
                    .ToListAsync();

                return Ok(_mapper.MapToCategoryGetVm(categories));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGetVm>> GetCategory(int id)
        {
            try
            {
                var category = await _context.Categories
                .FirstOrDefaultAsync(category => category.Id == id);

                if (category == null)
                    throw new NotFoundException(_localizer.GetString("NotFound_Category", id));

                return Ok(_mapper.MapToCategoryGetVm(category));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryGetVm>> PostCategory(CategoryPostVm categoryVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new BadRequestException(_localizer.GetString("InvalidObject"));

                var category = _mapper.MapToCategory(categoryVm);

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCategory", new { id = category.Id }, category);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoryGetVm>> DeleteCategory(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                    throw new NotFoundException(_localizer.GetString("NotFound_Category", id));

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return Ok(_mapper.MapToCategoryGetVm(category));
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
    }
}
