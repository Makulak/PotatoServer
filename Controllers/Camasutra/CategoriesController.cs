using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PotatoServer.Database;
using Microsoft.AspNetCore.Authorization;
using PotatoServer.ViewModels.Camasutra.Category;
using PotatoServer.Services.Mapping;
using PotatoServer.Exceptions;
using Microsoft.Extensions.Localization;
using PotatoServer.Helpers;
using PotatoServer.Filters;

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
        [BaseTypeFilter(typeof(LoggedActionAttribute))]
        public async Task<ActionResult<IEnumerable<CategoryGetVm>>> GetCategories([Minimum(0)] int? skip, [Minimum(0)] int? take)
        {
            var categories = await _context.Categories
                .GetPaged(skip, take)
                .ToListAsync();

            return Ok(_mapper.MapToCategoryGetVm(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGetVm>> GetCategory(int id)
        {
            var category = await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == id);

            if (category == null)
                throw new NotFoundException(_localizer.GetString("NotFound_Category", id));

            return Ok(_mapper.MapToCategoryGetVm(category));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryGetVm>> PostCategory(CategoryPostVm categoryVm)
        {
            var category = _mapper.MapToCategory(categoryVm);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryPutVm categoryVm)
        {
            if (id != categoryVm.Id)
                throw new BadRequestException(_localizer.GetString("BadRequest_IdsDoNotMatch", id));

            var categoryExists = await _context.Categories.AnyAsync(cat => cat.Id == id);
            if (!categoryExists)
                throw new NotFoundException(_localizer.GetString("NotFound_Category", id));

            var category = _mapper.MapToCategory(categoryVm);

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(_mapper.MapToCategoryGetVm(category));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoryGetVm>> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                throw new NotFoundException(_localizer.GetString("NotFound_Category", id));

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(_mapper.MapToCategoryGetVm(category));
        }
    }
}
