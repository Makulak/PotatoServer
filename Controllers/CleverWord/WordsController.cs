using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PotatoServer.Database;
using PotatoServer.Exceptions;
using PotatoServer.Services.Mapping;
using PotatoServer.ViewModels.CleverWord.Word;

namespace PotatoServer.Controllers.CleverWord
{
    [Route("api/clever-word/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly WordMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public WordsController(DatabaseContext context,
            WordMapper mapper,
            IStringLocalizer<SharedResources> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WordGetVm>>> GetWords()
        {
            var words = await _context.Words.ToListAsync();

            return Ok(_mapper.MapToWordGetVm(words));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WordGetVm>> GetWord(int id)
        {
            var word = await _context.Words.FindAsync(id);

            if (word == null)
                throw new NotFoundException(_localizer.GetString("NotFound_Word", id));

            return Ok(_mapper.MapToWordGetVm(word));
        }

        [HttpGet("random")]
        public async Task<ActionResult<WordGetVm>> GetRandomWord()
        {
            var word = await _context.Words.OrderBy(word => Guid.NewGuid()).FirstAsync();

            return Ok(_mapper.MapToWordGetVm(word));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<WordGetVm>> PostWord(WordPostVm wordVm)
        {
            var word = _mapper.MapToWord(wordVm);

            _context.Words.Add(word);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWord", new { id = word.Id }, word);
        }

        [HttpPost("add-many")]
        public async Task<ActionResult<List<int>>> PostWords(List<WordPostVm> wordsVm)
        {
            var words = _mapper.MapToWord(wordsVm);

            _context.Words.AddRange(words);
            await _context.SaveChangesAsync();

            return Ok(words.Select(word => word.Id));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<WordGetVm>> DeleteWord(int id)
        {
            var word = await _context.Words.FindAsync(id);
            if (word == null)
                throw new NotFoundException(_localizer.GetString("NotFound_Word", id));

            _context.Words.Remove(word);
            await _context.SaveChangesAsync();

            return _mapper.MapToWordGetVm(word);
        }
    }
}
